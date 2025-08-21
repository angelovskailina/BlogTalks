using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System.Security.Claims;

namespace BlogTalks.Application.Comments.Commands;

public class AddHandler(
    IBlogPostRepository blogPostRepository,
    ICommentRepository commentRepository,
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory httpClientFactory,
    IUserRepository userRepository,
    IFeatureManager featureManager,
    IServiceProvider serviceProvider,
    ILogger<AddHandler> logger)
    : IRequestHandler<AddRequest, AddResponse>
{
    public async Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        int? userIdValue = int.TryParse(userIdClaim?.Value, out var parsedUserId) ? parsedUserId : null;

        var blogPost = blogPostRepository.GetById(request.blogPostId);
        var comment = new Comment
        {
            BlogPostID = request.blogPostId,
            BlogPost = blogPost,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userIdValue.Value,
        };
        commentRepository.Add(comment);

        var blogpostCreator = userRepository.GetById(blogPost.CreatedBy);
        var commentCreator = userRepository.GetById(userIdValue.Value);

        await SendEmail(commentCreator, blogpostCreator, blogPost);

        return new AddResponse { Id = comment.Id };
    }

    private async Task SendEmail(User commentCreator, User blogpostCreator, BlogPost blogPost)
    {
        var dto = new EmailDto()
        {
            From = commentCreator.Email,
            To = blogpostCreator.Email,
            Subject = "New Comment Added",
            Body =
                $"A new comment has been added to the blog post '{blogPost.Title}' by user {commentCreator.Name}."
        };

        if (await featureManager.IsEnabledAsync("EmailHttpSender"))
        {
            var service = serviceProvider.GetRequiredKeyedService<IMessagingService>("MessagingHttpService");
            await service.Send(dto);

        }
        else if (await featureManager.IsEnabledAsync("EmailRabbitMQSender"))
        {
            var service = serviceProvider.GetRequiredKeyedService<IMessagingService>("MessagingRabbitMQService");
            await service.Send(dto);
        }
        else
        {
            logger.LogError("No email sender feature flag is enabled. Email will not be sent.");
        }
    }
}