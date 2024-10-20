using MediatR;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;

namespace NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly ITokenService _tokenService;

    public Handler(IRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar requisição

        try
        {
            var res = Validation.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível concluir a requisição", 500);
        }

        #endregion

        #region 02.Verificar usuário no banco

        User? user;

        try
        {
            user = await _repository.GetUserByEmail(request.Email, cancellationToken);
            if (user is null)
                return new Response("Usuário não encontrado", 404);
        }
        catch (Exception)
        {
            return new Response("Não foi possível concluir a requisição", 500);
        }

        #endregion

        #region 03.Checar a senha

        if (!user.Password.Challenge(request.Password))
            return new Response("Usuário ou senha inválidos", 401);

        #endregion

        #region 04.Verificar se a conta está ativa

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("A conta não está ativada", 403);
        }
        catch 
        {
            return new Response("Não foi possível verificar a conta", 500);            
        }

        #endregion

        #region 05. Retornar os dados

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray(),
                Token = _tokenService.Create(user)
            };
            return new Response(string.Empty, data);
        }
        catch (Exception)
        {
            return new Response("Não foi possível obter os dados do perfil", 500);            
        }

        #endregion
    }
}
