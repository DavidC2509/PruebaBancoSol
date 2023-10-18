

using Microsoft.OpenApi.Models;

namespace TrackFinance.Web.Extensions;

public static class InjectonExtensions
{

    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "David Prueba Banco Sol",
            Version = "v1",
            Description = "Backend BSol. \nPrueba Backend CQRS .net5 https://github.com/DavidC2509/backend-sitec. \nPrueba Backend CQRS ardalis Spec y .net 7 https://github.com/DavidC2509/walletCQRS.",
            
            Contact = new OpenApiContact
            {
                Name = "davidfernando.chavez777@gmail.com"
            },
        });
        c.EnableAnnotations();

    });
    }

}
