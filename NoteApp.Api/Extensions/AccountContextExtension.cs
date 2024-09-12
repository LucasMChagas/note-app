using MediatR;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using NoteApp.Infra.Contexts.AccountContext.UseCases.Create;

namespace NoteApp.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRepository, Repository>();
        builder.Services.AddTransient<IService, Service>();
    }

    public static void UseAccountEndpoints(this WebApplication app)
    {
        app.MapPost("api/v1/account", async (
            Request request, 
            IRequestHandler<Request, Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess 
                ? Results.Created("",result)
                : Results.Json(result, statusCode: result.Status);
        });
    }
}
