using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Models;

namespace SistemaStreaming.Data;

public class TableContext : DbContext
{
    public DbSet<UserModel> Usuario { get; set; }
    public DbSet<PlaylistModel> Playlist { get; set; }
    public DbSet<ConteudosModel> Conteudos { get; set; }
    public DbSet<filesModel> Files { get; set; }
    public DbSet<CriadorModel> Criador { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=usuario.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}