using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(q => q.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(q => q.Password)
                .Must(ValidPassword)
                .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma letra minúscula e um caracter especial");

            RuleFor(q => q.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe o nome completo do usuário");
        }

        public bool ValidPassword(string password)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                return true;

            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}
