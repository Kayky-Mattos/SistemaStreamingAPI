using System.ComponentModel.DataAnnotations;

namespace SistemaStreaming.Models;

public class ConteudosModel
{

public ConteudosModel() { } 

public ConteudosModel(string title,string type, Guid playlistid,string urlconteudo)
{
    Title = title;
    Type = type;
    PlayListId = playlistid;
    UrlConteudo = urlconteudo;
    ConteudoId = Guid.NewGuid();
}
    [Key]
    public  Guid ConteudoId             { get; init; } 
    public  Guid PlayListId             { get; init; } 
    public PlaylistModel? Playlist      { get; set; }
    public  string?  Title              { get; set; }
    public  string?  Type               { get; set; }
    public  string?  UrlConteudo        { get; set; }
}