using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Domain;
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

        #region Delete
        builder.Services.AddTransient<
            NoteApp.Domain.Contexts.NoteContext.UseCases.Update.Contracts.IRepository,
            NoteApp.Infra.Contexts.NoteContext.UseCases.Update.Repository>();
        #endregion

        #region GetById
        builder.Services.AddTransient<
            Domain.Contexts.NoteContext.UseCases.GetById.Contracts.IRepository,
            Infra.Contexts.NoteContext.UseCases.GetById.Repository>();
        #endregion

        #region GetAll
        builder.Services.AddTransient<
            Domain.Contexts.NoteContext.UseCases.GetAll.Contracts.IRepository,
            Infra.Contexts.NoteContext.UseCases.GetAll.Repository>();
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

        #region Update
        app.MapPut("api/v1/note/update/{id}", async (
            ClaimsPrincipal user,
            [FromRoute] string id,
            Domain.Contexts.NoteContext.UseCases.Update.Request request,
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.Update.Request,
                Domain.Contexts.NoteContext.UseCases.Update.Response> handler) =>
        {
            request.UserId = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty;
            request.NoteId = id ?? string.Empty;

            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion

        #region GetById
        app.MapGet("api/v1/note/{id}", async (
            ClaimsPrincipal user,
            [FromRoute] string id,            
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.GetById.Request,
                Domain.Contexts.NoteContext.UseCases.GetById.Response> handler) =>
        {
            Domain.Contexts.NoteContext.UseCases.GetById.Request request;
            request = new()
            {
                UserId = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty,
                NoteId = id ?? string.Empty
            };    

            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion

        #region GetAll
        app.MapGet("api/v1/note/get-all", async (
            ClaimsPrincipal user,
            [FromQuery] int page,
            [FromQuery] int pageSize,
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.GetAll.Request,
                Domain.Contexts.NoteContext.UseCases.GetAll.Response> handler) =>
        {
            Domain.Contexts.NoteContext.UseCases.GetAll.Request request = new();
            
            request.UserId = user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty;
            request.PageNumber = page != 0 ? page : request.PageNumber;
            request.PageSize = pageSize != 0 ? pageSize : request.PageSize;

            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion
    }
}
