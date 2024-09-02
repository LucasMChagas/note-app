using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Api;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using System.Security.Claims;
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

//Adiciona suporte a autorização e cria uma política de acesso
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", options => options.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/login", ([FromServices]TokenService service) => 
{
    var roles = new List<string>
    {
        "default",
        "admin"
    };
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

app.MapGet("/restrito", (ClaimsPrincipal user) =>
{
    //Busca no objeto uma claim do tipo especificado
    var id = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? "0";
    
    return $"Autorizado! id = {id}";
    
}).RequireAuthorization("admin");

app.Run();
