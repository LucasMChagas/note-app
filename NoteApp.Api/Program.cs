using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Extensions;
using NoteApp.Api.Models;
using NoteApp.Api.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddServices();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
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
