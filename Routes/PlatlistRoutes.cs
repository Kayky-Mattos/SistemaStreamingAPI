using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;

namespace SistemaStreaming.Routes;

public static class PlaylistRoutes
{
    public static void MapPlaylistRoutes(this WebApplication app)
    {
        const string routePlaylist = "v1/user/playlist";
        const string routePlaylistPost = "v1/user";
        const string playlist = "playlist";

        app.MapPost($"{routePlaylistPost}/{{UserId:guid}}/{playlist}", async (Guid UserId,PayloadPlaylistRequest req,TableContext context) =>
        {
            var user = await context.Usuario.FirstOrDefaultAsync(u => u.UserId == UserId);

            if (string.IsNullOrWhiteSpace(req.title) || user == null )
            {
                return Results.BadRequest("titulo em branco ou usuário não encontrado");
            }

            var playlist = new PlaylistModel(req.title,UserId);

            await context.Playlist.AddAsync(playlist);
            await context.SaveChangesAsync();
            
            return Results.Created($"/{routePlaylistPost}/{playlist.Title}", playlist);
        }).RequireAuthorization();

        app.MapPut($"{routePlaylist}/{{PlayListId:guid}}", async (Guid PlayListId,PayloadPlaylistRequest req,TableContext context) =>
        {
            var plist = await context.Playlist.FirstOrDefaultAsync(u => u.PlayListId == PlayListId);

            if (string.IsNullOrWhiteSpace(req.title) || plist == null )
            {
                return Results.BadRequest("titulo em branco ou playlist não encontrada");
            }
            plist.Title = req.title;
            await context.SaveChangesAsync();
            
            return Results.Ok(plist);
        }).RequireAuthorization();

        app.MapGet(routePlaylist, async (TableContext context) =>
        {
            var playlist = await context.Playlist
            .Include(u => u.Conteudos)
            .ToListAsync();
            return Results.Ok(playlist);
        }).RequireAuthorization();

        app.MapGet($"{routePlaylist}/{{PlayListId:guid}}", async (Guid PlayListId, TableContext context) =>
        {
            var PlayList = await context.Playlist
                .Include(u => u.Conteudos) 
                .Where(u => u.PlayListId == PlayListId)
                .FirstOrDefaultAsync();

            return PlayList == null ? Results.NotFound() : Results.Ok(PlayList);
        }).RequireAuthorization();        

        app.MapDelete($"{routePlaylist}/{{PlayListId:guid}}", async (Guid PlayListId, TableContext context) =>
        {
            var playlist = await context.Playlist.FirstOrDefaultAsync(x => x.PlayListId == PlayListId);

            if (playlist == null)
            {
                return Results.NotFound();
            }

            context.Playlist.Remove(playlist);
            await context.SaveChangesAsync();

            return Results.Ok("Playlist deletada com sucesso!");
        }).RequireAuthorization();
       
    }
}
