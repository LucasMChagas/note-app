using MediatR;
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
    }

    public static void UseNoteEndpoints(this WebApplication app)
    {
        #region Create
        app.MapPost("api/v1/create-note", async (
            ClaimsPrincipal user,
            NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Request request,
            IRequestHandler<
                Domain.Contexts.NoteContext.UseCases.Create.Request,
                Domain.Contexts.NoteContext.UseCases.Create.Response> handler) =>
        {
            request.UserId = new Guid(user.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Created("", result)
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();
        #endregion
    }
}
