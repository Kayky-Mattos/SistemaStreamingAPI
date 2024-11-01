using System.ComponentModel.DataAnnotations;

namespace SistemaStreaming.Models;

public class PlaylistModel
{

public PlaylistModel() { } 
public PlaylistModel(string title, Guid userid)
{
    Title = title;
    UserId = userid;
    PlayListId = Guid.NewGuid();
}
    [Key]
    public  Guid PlayListId   { get; init; } 
    public  Guid UserId       { get; init; } 
    public UserModel? User    { get; set; }
    public  string?  Title     { get; set; }
    public List<ConteudosModel> Conteudos { get; set; } = new List<ConteudosModel>();
}