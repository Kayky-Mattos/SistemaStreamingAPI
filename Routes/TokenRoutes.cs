using Microsoft.EntityFrameworkCore;
using SistemaStreaming.Data;
using SistemaStreaming.Models;
using SistemaStreaming.Utils;

namespace SistemaStreaming.Routes;

public static class TokenRoutes
{
    public static void MapTokenRoutes(this WebApplication app)
    {
        const string routeToken = "v1/getToken";

        app.MapGet($"{routeToken}", () =>
        {
            var token =  TokenService.GenerateToken();
            return Results.Ok(new { token = token });
        });
    }
}
