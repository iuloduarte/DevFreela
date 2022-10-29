using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.Projects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(q => q.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descrição é 255 caracteres");

            RuleFor(q => q.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é 30 caracteres");
        }
    }
}
