using System.ComponentModel.DataAnnotations;

namespace SistemaStreaming.Models;

public class filesModel
{

public filesModel() { } 

public filesModel(string title,string type, string fileurl,Guid criadorid)
{
    Title = title;
    Type = type;
    FileUrl = fileurl;
    CriadorId = criadorid;
    FileId = Guid.NewGuid();
}
    [Key]
    public  Guid FileId                 { get; init; } 
    public  Guid CriadorId              { get; init; } 
    public CriadorModel? Criador        { get; set; }
    public  string?  Title              { get; set; }
    public  string?  Type               { get; set; }
    public  string?  FileUrl            { get; set; }
}