using FluentValidation;
using ModsenTask.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Validators
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator() {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Имя автора должно быть указано")
                .MaximumLength(100)
                .WithMessage("Имя автора слишком длинное");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Фамилия автора должна быть указано")
                .MaximumLength(100)
                .WithMessage("Фамилия автора слишком длинная");
        }
    }
}
