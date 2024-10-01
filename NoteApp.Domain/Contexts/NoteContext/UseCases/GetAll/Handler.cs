using MediatR;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll.Contracts;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.GetAll;

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

        List<Note> notes;
        #region 02.Buscar as nota do usuário no banco de dados
        try
        {
            notes = await _repository
                .GetNotesAsync(
                    new Guid(request.UserId),
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);
        }
        catch (Exception ex)
        {
            return new Response($"Falha ao buscar as notas no banco de dados: {ex.Message}", 500);
        }
        #endregion

        return new Response("Requisição realizada com sucesso!", new ResponseData(notes));
    }
}
