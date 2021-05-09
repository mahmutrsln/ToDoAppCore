using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;

namespace YSKProje.Todo.Business.ValidationRules.FluetValidation
{
    public class RaporAddValidator : AbstractValidator<RaporAddDto>
    {
        public RaporAddValidator()
        {
            RuleFor(I => I.Tanim).NotNull().WithMessage("Tanım alanı gereklidir");
            RuleFor(I => I.Detay).NotNull().WithMessage("Detay alanı gereklidir");
        }
    }
}
