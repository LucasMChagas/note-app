using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using System.Security.Claims;

namespace NoteApp.Api.Extensions;

public static class NoteContextExtension
{
    public static void AddNoteContext(this WebApplicationBuilder builder)
    {
        #region Create
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Contracts.IRepository,
            NoteApp.Infra.Contexts.NoteContext.UseCases.Create.Repository>();
        #endregion

        #region Delete
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.NoteContext.UseCases.Delete.Contracts.IRepository,
            NoteApp.Infra.Contexts.NoteContext.UseCases.Delete.Repository>();
        #endregion
    }

    public static void UseNoteEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost("api/v1/note", async (
            ClaimsPrincipal user,
            NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Request request,
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.Create.Request,
                Domain.Contexts.NoteContext.UseCases.Create.Response> handler) =>
        {
            string userId = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty;

            request.UserId = new Guid( userId ?? string.Empty);         
            
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created("", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion

        #region Delete
        app.MapDelete("api/v1/note/delete/{id}", async (
            ClaimsPrincipal user,
            [FromRoute]string id,
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.Delete.Request,
                Domain.Contexts.NoteContext.UseCases.Delete.Response> handler) =>
        {
            string userId = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty;

            try
            {
                Domain.Contexts.NoteContext.UseCases.Delete.Request request = new()
                {
                    UserId = new Guid(userId),
                    NoteId = new Guid(id)
                };
                var result = await handler.Handle(request, new CancellationToken());
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: result.Status);
            }
            catch (FormatException ex) 
            {
                return Results.BadRequest(
                    new Domain.Contexts.NoteContext.UseCases.Delete.Response(ex.Message, 400));
            }            
        }).RequireAuthorization();
        #endregion
    }
}
