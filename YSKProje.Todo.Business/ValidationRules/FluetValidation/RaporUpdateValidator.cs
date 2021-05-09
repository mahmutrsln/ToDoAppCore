using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;

namespace YSKProje.Todo.Business.ValidationRules.FluetValidation
{
    public class RaporUpdateValidator : AbstractValidator<RaporUpdateDto>
    {
        public RaporUpdateValidator()
        {
            RuleFor(I => I.Tanim).NotNull().WithMessage("Tanım alanı gereklidir");
            RuleFor(I => I.Detay).NotNull().WithMessage("Detay alanı gereklidir");
        }
    }
}
