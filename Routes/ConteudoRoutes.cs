using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;

namespace SistemaStreaming.Routes;

public static class ConteudoRoutes
{
    public static void MapConteudoRoutes(this WebApplication app)
    {
        const string RouteConteudo = "v1/user/playlist/conteudo";
        const string RouteConteudoPost = "v1/user/playlist";
        const string conteudo = "Conteudo";

        app.MapPost($"{RouteConteudoPost}/{{PlayListId:guid}}/{conteudo}", async (Guid PlayListId,PayloadConteudosRequest req,TableContext context) =>
        {
           
            if (string.IsNullOrWhiteSpace(req.title) || string.IsNullOrWhiteSpace(req.type) )
            {
                return Results.BadRequest("Titulo ou tipo não inseridos!");
            }

            var Conteudos = new ConteudosModel(req.title,req.type,PlayListId,req.urlconteudo);

            await context.Conteudos.AddAsync(Conteudos);
            await context.SaveChangesAsync();
            
            return Results.Created($"/{RouteConteudoPost}/{Conteudos.Title}", Conteudos);
        }).RequireAuthorization();     

        app.MapPut($"{RouteConteudo}/{{ConteudoId:guid}}", async (Guid ConteudoId,PayloadConteudosRequest req,TableContext context) =>
        {
            var conteudos = await context.Conteudos.FirstOrDefaultAsync(u => u.ConteudoId == ConteudoId);

            if (string.IsNullOrWhiteSpace(req.title) || string.IsNullOrWhiteSpace(req.type) || conteudos == null )
            {
                return Results.BadRequest("titulo ou tipo em branco ou playlist não encontrada");
            }
            conteudos.Title = req.title;
            conteudos.Type = req.type;
            await context.SaveChangesAsync();
            
            return Results.Ok(conteudos);
        }).RequireAuthorization();

        app.MapGet(RouteConteudo, async (TableContext context) =>
        {
            var Conteudos = await context.Conteudos
            .ToListAsync();
            return Results.Ok(Conteudos);
        }).RequireAuthorization();

        app.MapGet($"{RouteConteudo}/{{ConteudoId:guid}}", async (Guid ConteudoId, TableContext context) =>
        {
            var Conteudo = await context.Conteudos
                .Where(u => u.ConteudoId == ConteudoId)
                .FirstOrDefaultAsync();

            return Conteudo == null ? Results.NotFound() : Results.Ok(Conteudo);
        }).RequireAuthorization();        

        app.MapDelete($"{RouteConteudo}/{{ConteudoId:guid}}", async (Guid ConteudoId, TableContext context) =>
        {
            var conteudo = await context.Conteudos.FirstOrDefaultAsync(x => x.ConteudoId == ConteudoId);

            if (conteudo == null)
            {
                return Results.NotFound();
            }

            context.Conteudos.Remove(conteudo);
            await context.SaveChangesAsync();

            return Results.Ok("Conteudo deletado com sucesso!");
        }).RequireAuthorization();
       
    }
}
