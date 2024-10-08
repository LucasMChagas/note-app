﻿using Microsoft.IdentityModel.Tokens;
using NoteApp.Domain;
using NoteApp.Domain.Contexts.AccountContext.Entities;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate;
using NoteApp.Domain.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteApp.Api.Services;

public class TokenService : ITokenService
{
    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);        

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaims(user)
        };        

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim("id", user.Id.ToString()));        
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

        foreach (var role in user.Roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }

        return claimsIdentity;
    }
}
