using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Api;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
}).AddJwtBearer(jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/login", ([FromServices]TokenService service) => 
{
    var roles = new List<string>();
    roles.Add("default");
    roles.Add("admin");
    var user = new User
    {
        Name = "Lucas",
        UserName = "lmc23",
        Email = "l@gmail.com",
        Password = "12345",
        Id = 1,
        Image = "asddasasd",
        Roles = roles
    };
    return service.Create(user);
});

app.MapGet("/restrito", () =>
{
    return "Autorizado!";
}).RequireAuthorization();

app.Run();
