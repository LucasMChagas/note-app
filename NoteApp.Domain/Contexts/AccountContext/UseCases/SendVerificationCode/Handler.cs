using MediatR;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode.Contracts;
using NoteApp.Domain.Services;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.SendVerificationCode;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
	private readonly ISendEmailService _service;

    public Handler(IRepository repository, ISendEmailService service)
	{
        _repository = repository;
		_service = service;
    }
        
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01.Validar requisição
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
        #endregion

        #region 02. Buscar conta no banco de dados
        User user;

		try
		{
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Usuário não cadastrado", 404);
        }
		catch 
		{
            return new Response("Erro ao verificar a base de dados", 500);
        }
        #endregion

        #region 03. Verificar status da conta e enviar email de ativação
        try
        {
			if (user.Email.Verification.VerifiedAt != null)
				return new Response("Está conta já foi ativada!", 400);

			user.Email.Verification.NewCode();
			await _repository.UpdateAsync(user, cancellationToken);
			await _service.SendVerificationEmailAsync(user, cancellationToken);
		}
		catch 
		{
			return new Response("Falha ao gerar um novo código de verificação!", 500);			
		}
        #endregion

        return new Response("Link de verificação enviado para o email informado!", 200);

    }
}
