using FluentValidation;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Users.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserModel.Name).NotEmpty().MaximumLength(User.NameMaxLenght);
        }
    }
}
