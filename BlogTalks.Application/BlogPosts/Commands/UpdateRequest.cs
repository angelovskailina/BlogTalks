﻿using MediatR;
using System.Text.Json.Serialization;

namespace BlogTalks.Application.BlogPosts.Commands
{
    public record UpdateRequest([property: JsonIgnore] int Id, string Title, string Text, List<string> Tags) : IRequest<UpdateResponse>;
}
