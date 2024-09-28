using MediatR;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Update.Contracts;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Update;

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
                new Guid(request.UserId),
                new Guid(request.NoteId),
                cancellationToken);

            if (!isAuthor)
                return new Response("Acesso negado! Você não tem autorização para acessar esse recurso.", 403);
        }
        catch(Exception ex) 
        {
            return new Response($"Falha interna do servidor: {ex.Message}", 500);
        }
        #endregion

        #region 03.Atualizar nota no banco de dados
        try
        {
            await _repository.UpdateAsync(
                new Guid(request.NoteId),
                request.Title, 
                request.Body, 
                cancellationToken);
        }
        catch(Exception ex)
        {
            return new Response($"Falha ao editar a nota: {ex.Message}", 500);
        }
        #endregion

        return new Response(
            "Nota editada com sucesso!",
            new ResponseData(
                request.Title,
                request.Body,
                DateTime.Now));
    }
}
