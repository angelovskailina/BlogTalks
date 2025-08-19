using BlogTalks.API;
using BlogTalks.API.Middlewares;
using BlogTalks.Application;
using BlogTalks.Infrastructure;
using Serilog;
using System.Buffers.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(@"c:\logs\myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);

});

builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("EmailSenderApi", client =>
{
    var config = builder.Configuration.GetSection("Services:EmailSenderApi");
    client.BaseAddress = new Uri(config["Url"]);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
