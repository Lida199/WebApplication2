using WebApplication2.Models;
using FluentValidation;
namespace WebApplication2.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {

            RuleFor(p => p.CreateDate)
               .NotEmpty()
               .WithMessage("Please indicate the CreateDate.")
               .Must(date => date.Date <= DateTime.Today)
               .WithMessage("CreateDate must not be in the future.");

            RuleFor(p => p.FirstName)
               .NotEmpty()
               .WithMessage("FirstName cannot be empty.")
               .MaximumLength(50)
               .WithMessage("FirstName cannot be more than 50 characters of length.");

            RuleFor(p => p.LastName)
               .NotEmpty()
               .WithMessage("LastName cannot be empty.")
               .MaximumLength(50)
               .WithMessage("LastName cannot be more than 50 characters of length.");

            RuleFor(p => p.JobPosition)
               .NotEmpty()
               .WithMessage("JobPosition cannot be empty.")
               .MaximumLength(50)
               .WithMessage("JobPosition cannot be more than 50 characters of length.");

            RuleFor(p => p.Salary)
                .InclusiveBetween(0,10000)
                .WithMessage("Salary is outside the range(0-10000).");

            RuleFor(p => p.WorkExperience)
                .NotEmpty()
                .WithMessage("Please indicate the WorkExperience.");

            RuleFor(p => p.PersonAddress.Country)
                .NotEmpty().WithMessage("Please indicate the Country.");

            RuleFor(p => p.PersonAddress.City)
                 .NotEmpty().WithMessage("Please indicate the City.");

            RuleFor(a => a.PersonAddress.HomeNumber)
                 .NotEmpty().WithMessage("Please indicate the HomeNumber.");
        }
    }
}
