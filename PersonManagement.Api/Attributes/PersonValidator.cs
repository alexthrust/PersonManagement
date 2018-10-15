using System;
using FluentValidation;
using FluentValidation.Validators;
using PersonManagement.Services.Abstraction;
using PersonManagement.Services.Models;

namespace PersonManagement.Api.Attributes
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        private readonly IPersonService _personService;

        public PersonValidator(IPersonService personService)
        {
            _personService = personService;

            RuleFor(m => m.FirstName).NotEmpty();

            RuleFor(m => m.LastName).NotEmpty();

            RuleFor(m => m.PersonalNumber).Length(11).When(m => !string.IsNullOrEmpty(m.PersonalNumber));
            RuleFor(m => m.PersonalNumber).SetValidator(new RegularExpressionValidator("[0-9]")).When(m => !string.IsNullOrEmpty(m.PersonalNumber));
            RuleFor(m => m.PersonalNumber).Custom((list, context) =>
            {
                if (context.PropertyValue != null && string.IsNullOrEmpty(context.PropertyValue.ToString())) return;

                var result = _personService.IsPersonalNumberUnique(context.PropertyValue.ToString(), ((PersonModel)context.InstanceToValidate).Id);

                if(!result.Data)
                    context.AddFailure("'Personal Number' should be unique.");
            });

            RuleFor(m => m.Salary).GreaterThanOrEqualTo(0).When(m => !string.IsNullOrEmpty(m.PersonalNumber));
        }
    }
}