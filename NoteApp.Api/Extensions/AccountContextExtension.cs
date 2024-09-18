using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts.ITokenService,
            NoteApp.Api.Services.TokenService>();
        #endregion

        #region AccountVerification
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Contracts.IRepository,
            NoteApp.Infra.Contexts.AccountContext.UseCases.AccountVerification.Repository>();
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

            if(!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            if (result.Data is null)
                return Results.Json(result, statusCode: 500);
            
            return Results.Ok(result);                
        });

        #endregion

        app.MapPost("api/v1/teste", async (ClaimsPrincipal user) => new
        {
            user.Identity.Name,
            id = user.Claims.FirstOrDefault(x=> x.Type == "id").Value
        }).RequireAuthorization("admin");

        app.MapPost("api/v1/verification", async (            
            NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Request request,
            IRequestHandler<
                NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Request,
                NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Response> handler) =>
        { 
            var result = await handler.Handle(request, new CancellationToken());

            if (!result.IsSuccess)
                return Results.Json(result, statusCode: result.Status);

            return Results.Ok(result);
        });
    }
}
