using MediatR;

namespace NoteApp.Api.Extensions;

public static class AccountContextExtension
{
    public static void AddAccountContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IRepository,
            NoteApp.Infra.Contexts.AccountContext.UseCases.Create.Repository>();
        
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts.IService,
            NoteApp.Infra.Contexts.AccountContext.UseCases.Create.Service>();
        #endregion

        #region Authenticate
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.IRepository,
            NoteApp.Infra.Contexts.AccountContext.UseCases.Authenticate.Repository>();
        #endregion
    }

    public static void UseAccountEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost("api/v1/account", async (
            NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Request request, 
            IRequestHandler<
                Domain.Contexts.AccountContext.UseCases.Create.Request,
                Domain.Contexts.AccountContext.UseCases.Create.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess 
                ? Results.Created("",result)
                : Results.Json(result, statusCode: result.Status);
        });
        #endregion

        #region Authenticate

        app.MapPost("api/v1/authenticate", async (
            NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Request request,
            IRequestHandler<
                Domain.Contexts.AccountContext.UseCases.Authenticate.Request,
                Domain.Contexts.AccountContext.UseCases.Authenticate.Response > handler ) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode:result.Status);
        });

        #endregion
    }
}
