using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Delete.Contracts;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Delete;

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
            var isAuthor = await _repository.UserIsAuthorAsync(request.UserId,request.NoteId, cancellationToken);
            if (!isAuthor)
                return new Response("Acesso negado", 403);
        }
        catch
        {
            return new Response("Falha interna do servidor", 500);
        }
        #endregion

        #region 03.Deletar no banco de dados
        try
        {
            await _repository.DeleteAsync(request.NoteId, cancellationToken);
        }
        catch
        {
            return new Response("Falha deletar os dados", 500);
        }
        #endregion

        return new Response("Nota excluída com sucesso!", 200);
    }
}
