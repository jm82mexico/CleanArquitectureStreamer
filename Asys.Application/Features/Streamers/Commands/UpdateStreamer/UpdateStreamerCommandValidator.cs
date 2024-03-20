using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Asys.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator :AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} debe ser mayor a 0.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} debe tener un máximo de {Maxlength} caracteres.");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacío.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} debe tener un máximo de {MaxLength} caracteres.");
        }
    }

}