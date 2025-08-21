using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;
using System.Net.Http.Json;

namespace BlogTalks.Infrastructure.Messaging;

public class MessagingHttpService(IHttpClientFactory httpClientFactory) : IMessagingService
{
    public async Task Send(EmailDto email)
    {
        var client = httpClientFactory.CreateClient("EmailSenderApi");
        await client.PostAsJsonAsync("/email", email);

    }

}