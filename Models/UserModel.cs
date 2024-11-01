using System.ComponentModel.DataAnnotations;

namespace SistemaStreaming.Models;

public class UserModel
{

public UserModel(string name, string email, string senha)
{
    Name = name;
    Email = email;
    Senha = senha;
    UserId = Guid.NewGuid();
    Salt = Guid.NewGuid();
}
    [Key]
    public  Guid UserId   { get; init; } 
    public  string  Name { get; set; }
    public  string  Email { get; set; }
    public string Senha { get; set; }
    public Guid Salt { get; set; }
    public List<PlaylistModel> Playlist { get; set; } = new List<PlaylistModel>();
}