using MediatR;
using NoteApp.Domain.AccountContext.ValueObjects;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using NoteApp.Domain.Contexts.AccountContext.ValueObjects;
using NoteApp.Domain.Services;


namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly ISendEmailService _service;

    public Handler(IRepository repository, ISendEmailService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task<Response> Handle(
        Request request, 
        CancellationToken cancellationToken)
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

        #region 02.Geraração dos objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
            
            user.Email.Verification.Verify(user.Email.Verification.Code);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, status: 500);            
        }

        #endregion

        #region 03.Verifica se o usuário existe

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este email já está em uso", 409);
        }
        catch
        {
            return new Response("Falha ao verificar o e-mail cadastrado", 500);            
        }

        #endregion

        #region 04.Persistir os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir os dados", 500);
        }

        #endregion

        #region 05.Enviar email de ativação

        try
        {
            
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao enviar o email de verificação", 500);
        }

        #endregion

        return new Response(
            "Conta criada!",
            new ResponseData(user.Id, user.Name, user.Email));

    }


}
