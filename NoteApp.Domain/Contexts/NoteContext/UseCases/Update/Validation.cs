﻿using Flunt.Notifications;
using Flunt.Validations;

namespace NoteApp.Domain.Contexts.NoteContext.UseCases.Update;

public static class Validation
{
    public static Contract<Notification> Ensure(Request request)
    => new Contract<Notification>()
           .Requires()
           .IsLowerThan(request.Title.Length, 20, "Título", "O Título da nota deve conter menos do que 25 caracteres.")
           .IsNotEmpty(request.Title, "Título", "O Título da nota deve ser fornecido!")
           .IsLowerThan(request.Body.Length, 500, "Code", "O tamanho da nota não deve ultrapassar 500 caracteres.")
           .IsNotEmpty(request.Body, "Corpo", "O conteúdo da nota não foi fornecido")
           .IsLowerThan(request.NoteId, 37, "Id Nota", "Formato de Id da nota errado!")
           .IsGreaterThan(request.NoteId, 35, "Id Nota", "Formato de Id da nota errado!")
           .IsLowerThan(request.UserId, 37, "Id da conta", "Formato de Id da conta errado!")
           .IsGreaterThan(request.UserId, 35, "Id da conta", "Formato de Id da conta errado!");
}
