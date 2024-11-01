using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;
using SistemaStreaming.Utils;

namespace SistemaStreaming.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        const string routeUser = "v1/user";

        app.MapPost(routeUser, async (PayloadUserRequest req, TableContext context) =>
        {
            if (string.IsNullOrWhiteSpace(req.name) || string.IsNullOrWhiteSpace(req.email) || string.IsNullOrWhiteSpace(req.senha))
            {
                return Results.BadRequest("Nome ou email em branco!");
            }

            var user = new UserModel(req.name,req.email,req.senha);
            
            var saltedPassword = req.senha + user.Salt.ToString();
            var passwordHashed = HashPassword.GenerateHash(saltedPassword);  
            user.Senha = passwordHashed;          

            await context.AddAsync(user);
            await context.SaveChangesAsync();
            
            return Results.Created($"/{routeUser}/{user.UserId}", user);
        }).RequireAuthorization();

        app.MapGet(routeUser, async (TableContext context) =>
        {
            var users = await context.Usuario
            .Include(u => u.Playlist)
            .ThenInclude(c => c.Conteudos)
            .ToListAsync();
            return Results.Ok(users);
        }).RequireAuthorization();

        app.MapGet($"{routeUser}/{{UserId:guid}}", async (Guid UserId, TableContext context) =>
        {
            var user = await context.Usuario
                .Include(u => u.Playlist) 
                .ThenInclude(c => c.Conteudos)
                .Where(u => u.UserId == UserId)
                .FirstOrDefaultAsync();

            return user == null ? Results.NotFound() : Results.Ok(user);
        }).RequireAuthorization();

        app.MapPut($"{routeUser}/{{UserId:guid}}", async (Guid UserId, PayloadUserRequest req, TableContext context) =>
        {
            var user = await context.Usuario.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (user == null)
            {
                return Results.NotFound();
            }

            user.Name = req.name;
            user.Email = req.email;
            if (!string.IsNullOrWhiteSpace(req.senha))
            {            
                var saltedPassword = req.senha + user.Salt.ToString();
                var passwordHashed = HashPassword.GenerateHash(saltedPassword);  
                user.Senha = passwordHashed; 
            }           
            await context.SaveChangesAsync();

            return Results.Ok(user);
        }).RequireAuthorization();

        app.MapDelete($"{routeUser}/{{UserId:guid}}", async (Guid UserId, TableContext context) =>
        {
            var user = await context.Usuario.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (user == null)
            {
                return Results.NotFound();
            }

            context.Usuario.Remove(user);
            await context.SaveChangesAsync();

            return Results.Ok("Usuario deletado com sucesso!");
        }).RequireAuthorization();
    }
}
