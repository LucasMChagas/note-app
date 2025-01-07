using MediatR;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.Entities;
using NoteApp.Domain.Contexts.NoteContext.UseCases.Create.Contracts;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Create;

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
            request.Title = request.Title ?? string.Empty;
            request.Body = request.Body ?? string.Empty;

            var res = Validation.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida!", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar a requisição!", 500);
        }
        #endregion

        #region 02.Verificar se o usuário existe
        try
        {
            var exists = await _repository.AnyAsync(request.UserId, cancellationToken);
            if (!exists)
                return new Response("Id de usuário inválido!", 400);
        }
        catch
        {
            return new Response("Falha ao verificar o usuário", 500);
        }
        #endregion

        #region 03.Gerar o objeto
        Note note;
        try
        {
            note = new(request.Title, request.Body, request.UserId);            
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }
        #endregion

        #region 04.Persistir no banco de dados
        try
        {
            await _repository.SaveAsync(note, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir os dados", 500);
        }
        #endregion

        return new Response(
            "Nota criada com sucesso!",
            new ResponseData(note.Title, note.Body, note.UserId.ToString(), note.CreatedAt, note.Id.ToString()));
    }
}
