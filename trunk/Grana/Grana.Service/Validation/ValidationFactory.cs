using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Grana.Service.Validation.Interfaces;

namespace Grana.Service.Validation
{
    public class ValidationFactory<TModel> : AbstractValidator<TModel>, IValidationFactory
    {
    }
}
