﻿using BlogTalks.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace BlogTalks.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "ValidationFailure",
                    Title = "Balidation error",
                    Detail = "One or more validation errors has occured",
                };

                if (exception.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exception.Errors
                        .GroupBy(
                        x => x.PropertyName,
                        (key, group) => new Dictionary<string, string[]> { { key, group.Select(x => x.ErrorMessage).Distinct().ToArray() } });
                }
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (BlogTalksException exception)
            {
                context.Response.StatusCode = (int)exception.StatusCode;
                await context.Response.WriteAsync(exception.Message);
            }
        }
    }
}
