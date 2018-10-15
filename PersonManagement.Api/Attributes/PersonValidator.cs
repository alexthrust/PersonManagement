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

            RuleFor(m => m.PersonalNumber).Length(11);
            RuleFor(m => m.PersonalNumber).SetValidator(new RegularExpressionValidator("[0-9]"));
            RuleFor(m => m.PersonalNumber).Custom((list, context) =>
            {
                if (context.PropertyValue == null) return;

                var result = _personService.IsPersonalNumberUnique(context.PropertyValue.ToString());

                if(!result.Data)
                    context.AddFailure("'Personal Number' should be unique.");
            });

            RuleFor(m => m.Salary).GreaterThanOrEqualTo(0);
        }
    }
}