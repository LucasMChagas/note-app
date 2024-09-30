using MediatR;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.GetById.Contracts;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetById;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01.Validar requisição
        try
        {
            var res = Validation.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida!", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar a requisição!", 500);
        }
        #endregion

        #region 02.Verificar se o usuário é autor da nota
        try
        {

            var isAuthor = await _repository.UserIsAuthorAsync(
                new Guid(request.NoteId),
                new Guid(request.UserId),
                cancellationToken);

            if (!isAuthor)
                return new Response("Acesso negado! Você não tem autorização para acessar esse recurso.", 403);
        }
        catch (Exception ex)
        {
            return new Response($"Falha interna do servidor: {ex.Message}", 500);
        }
        #endregion

        Note note;
        #region 03.Buscar nota no banco de dados
        try
        {
            note = await _repository.GetByIdAsync(
                new Guid(request.NoteId),                
                cancellationToken);
        }
        catch (Exception ex)
        {
            return new Response($"Falha ao buscar a nota no banco de dados: {ex.Message}", 500);
        }
        #endregion

        return new Response("Requisição realizada com sucesso!", new ResponseData(note));

    }
}
