
using MediatR;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification.Contracts;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.AccountVerification;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
		try
		{
            var res = Validation.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
		catch 
		{
            return new Response("Não foi possível validar a requisição!", 500);
        }

        User user;

        try
        {
            user = await _repository.GetUserByEmail(request.Email, cancellationToken);
        }
        catch 
        {
            return new Response("Erro ao recuperar a conta", 500);
        }        

        if (user is null)
            return new Response("Conta não cadastrada!", 404);

        try
        {
            user.Email.Verification.Verify(request.Code);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        try
        {
            await _repository.UpdateAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Erro interno do servidor", 500);
        }        

        return new Response("Conta ativada com sucesso", 200);       
    }
}
