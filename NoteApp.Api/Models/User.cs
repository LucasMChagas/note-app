namespace NoteApp.Api.Models;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; } = [];
}
