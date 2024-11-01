using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;

namespace SistemaStreaming.Routes;

public static class FileRoutes
{
    public static void MapFileRoutes(this WebApplication app)
    {
        const string routeFile = "v1/criador/files";
        const string routeFilePost = "v1/criador";
        const string file = "files";

        app.MapPost($"{routeFilePost}/{{CriadorId:guid}}/{file}", async (Guid CriadorId,PayloadFileRequest req,TableContext context) =>
        {
            var criador = await context.Criador.FirstOrDefaultAsync(u => u.CriadorId == CriadorId);

            if (string.IsNullOrWhiteSpace(req.title) || string.IsNullOrWhiteSpace(req.type) || string.IsNullOrWhiteSpace(req.fileurl) || criador == null )
            {
                return Results.BadRequest("titulo tipo ou url em branco ou usuário não encontrado");
            }

            var file = new filesModel(req.title,req.type,req.fileurl,CriadorId);

            await context.Files.AddAsync(file);
            await context.SaveChangesAsync();
            
            return Results.Created($"/{routeFilePost}/{file.Title}", file);
        }).RequireAuthorization();

        app.MapPut($"{routeFile}/{{FileId:guid}}", async (Guid FileId,PayloadFileRequest req,TableContext context) =>
        {
            var plist = await context.Files.FirstOrDefaultAsync(u => u.FileId == FileId);

            if (string.IsNullOrWhiteSpace(req.title) || string.IsNullOrWhiteSpace(req.type) || string.IsNullOrWhiteSpace(req.fileurl) || plist == null )
            {
                return Results.BadRequest("titulo em branco ou playlist não encontrada");
            }
            plist.Title = req.title;
            plist.Type = req.type;
            plist.FileUrl = req.fileurl;
            await context.SaveChangesAsync();
            
            return Results.Ok(plist);
        }).RequireAuthorization();

        app.MapGet(routeFile, async (TableContext context) =>
        {
            var playlist = await context.Files
            .ToListAsync();
            return Results.Ok(playlist);
        }).RequireAuthorization();

        app.MapGet($"{routeFile}/{{FileId:guid}}", async (Guid FileId, TableContext context) =>
        {
            var PlayList = await context.Playlist
                .FirstOrDefaultAsync();

            return PlayList == null ? Results.NotFound() : Results.Ok(PlayList);
        }).RequireAuthorization();        

        app.MapDelete($"{routeFile}/{{FileId:guid}}", async (Guid FileId, TableContext context) =>
        {
            var file = await context.Files.FirstOrDefaultAsync(x => x.FileId == FileId);

            if (file == null)
            {
                return Results.NotFound();
            }

            context.Files.Remove(file);
            await context.SaveChangesAsync();

            return Results.Ok("Arquivo deletado com sucesso!");
        }).RequireAuthorization();

        app.MapPost("v1/upload/videos/", async (IFormFile file, IBlob blob) =>
        {
             var url = await blob.Upload(file);
             return new { url };
        }).RequireAuthorization().DisableAntiforgery();;
       
    }
}
