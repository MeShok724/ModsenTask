using FluentValidation;
using ModsenTask.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Validators
{
    public class BookRequestValidator : AbstractValidator<BookRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название книги обязательно")
                .MaximumLength(250).WithMessage("Название книги слишком длинное");
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN обязателен")
                .MaximumLength(13).WithMessage("ISBN не должен быть длиннее 13 символов");
            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("Автор книги обязателен");
        }
    }
}
