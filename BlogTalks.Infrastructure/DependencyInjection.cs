using BlogTalks.Application.Abstractions;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Authentication;
using BlogTalks.Infrastructure.Data.DataContext;
using BlogTalks.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogTalks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(
                options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

            services.AddScoped<ApplicationDbContext>();
            services.AddTransient<IBlogPostRepository, BlogPostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();


            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = jwtSettings.ValidateIssuer,
                       ValidateAudience = jwtSettings.ValidateAudience,
                       ValidateLifetime = jwtSettings.ValidateLifetime,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtSettings.Issuer,
                       ValidAudience = jwtSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            services.AddAuthorization();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
