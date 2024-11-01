using System.ComponentModel.DataAnnotations;

namespace SistemaStreaming.Models;

public class CriadorModel
{

public CriadorModel(string name, string email, string senha)
{
    Name        = name;
    Email       = email;
    Senha       = senha;
    CriadorId   = Guid.NewGuid();
    Salt        = Guid.NewGuid();
}
    [Key]
    public  Guid CriadorId   { get; init; } 
    public  string  Name { get; set; }
    public  string  Email { get; set; }
    public string Senha { get; set; }
    public Guid Salt { get; set; }
    public List<filesModel>? Files { get; set; } = new List<filesModel>();
}