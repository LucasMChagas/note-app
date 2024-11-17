using NoteApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(
    options => options.AddPolicy(
        "Wasm",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod() 
    ));
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddAccountContext();
builder.AddNoteContext();
builder.AddMediatR();

var app = builder.Build();
app.UseCors("Wasm");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAccountEndpoints();
app.UseNoteEndpoints();

app.Run();
public partial class Program { }
