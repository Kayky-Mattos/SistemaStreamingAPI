using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;
using SistemaStreaming.Utils;

namespace SistemaStreaming.Routes;

public static class CriadorRoutes
{
    public static void MapCriadorRoutes(this WebApplication app)
    {
        const string routeCriador = "v1/criador";

        app.MapPost(routeCriador, async (PayloadCriadorRequest req, TableContext context) =>
        {
            if (string.IsNullOrWhiteSpace(req.name) || string.IsNullOrWhiteSpace(req.email) || string.IsNullOrWhiteSpace(req.senha))
            {
                return Results.BadRequest("Nome ou email ou senha em branco!");
            }

            var criador = new CriadorModel(req.name,req.email,req.senha);
            
            var saltedPassword = req.senha + criador.Salt.ToString();
            var passwordHashed = HashPassword.GenerateHash(saltedPassword);  
            criador.Senha = passwordHashed;          

            await context.AddAsync(criador);
            await context.SaveChangesAsync();
            
            return Results.Created($"/{routeCriador}/{criador.CriadorId}", criador);
        }).RequireAuthorization();

        app.MapGet(routeCriador, async (TableContext context) =>
        {
            var criador = await context.Criador
            .Include(u => u.Files)
            .ToListAsync();
            return Results.Ok(criador);
        }).RequireAuthorization();

        app.MapGet($"{routeCriador}/{{CriadorId:guid}}", async (Guid CriadorId, TableContext context) =>
        {
            var criador = await context.Criador
                .Include(u => u.Files) 
                .Where(u => u.CriadorId == CriadorId)
                .FirstOrDefaultAsync();

            return criador == null ? Results.NotFound() : Results.Ok(criador);
        }).RequireAuthorization();

        app.MapPut($"{routeCriador}/{{CriadorId:guid}}", async (Guid CriadorId, PayloadCriadorRequest req, TableContext context) =>
        {
            var criador = await context.Criador.FirstOrDefaultAsync(x => x.CriadorId == CriadorId);

            if (criador == null)
            {
                return Results.NotFound();
            }

            criador.Name = req.name;
            criador.Email = req.email;
            if (!string.IsNullOrWhiteSpace(req.senha))
            {
                var saltedPassword = req.senha + criador.Salt.ToString();
                var passwordHashed = HashPassword.GenerateHash(saltedPassword);  
                criador.Senha = passwordHashed; 
            }        
            await context.SaveChangesAsync();

            return Results.Ok(criador);
        }).RequireAuthorization();

        app.MapDelete($"{routeCriador}/{{CriadorId:guid}}", async (Guid CriadorId, TableContext context) =>
        {
            var criador = await context.Criador.FirstOrDefaultAsync(x => x.CriadorId == CriadorId);

            if (criador == null)
            {
                return Results.NotFound();
            }

            context.Criador.Remove(criador);
            await context.SaveChangesAsync();

            return Results.Ok("Criador deletado com sucesso!");
        }).RequireAuthorization();
    }
}
