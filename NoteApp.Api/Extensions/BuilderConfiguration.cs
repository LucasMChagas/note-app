using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Api.Services;
using NoteApp.Infra.Data;
using System.Text;

namespace NoteApp.Api.Extensions;

public static class BuilderConfiguration
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.Database.ConnectionString =
        builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        Configuration.Secrets.JwtPrivateKey = 
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(
                Configuration.Database.ConnectionString
                /*, optionsBuilder => optionsBuilder.MigrationsAssembly("NoteApp.Api")*/));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = true;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("admin", options => options.RequireRole("admin"));
        });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<TokenService>();
    }

}
