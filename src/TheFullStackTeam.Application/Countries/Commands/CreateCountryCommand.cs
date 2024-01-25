using FluentValidation;
using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Countries.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateCountryCommand : IRequest<CreateCountryResult>
{
    public CountryModel Model { get; set; }

    public CreateCountryCommand(CountryModel model)
    {
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CreateCountryResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public CreateCountryHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCountryResult> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        Country entity = request.Model;
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCountryResult(entity!);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateCountryResult : AppResult<CountryListItem>
{
    public CreateCountryResult(CountryListItem response) : base(response)
    {
    }
}

/// <summary>
/// Fluent Command validation
/// </summary>
public sealed class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(x => x.Model.CommonName).NotNull().MaximumLength(Country.CommonNameMaxLenght);
        RuleFor(x => x.Model.NativeName).NotNull().MaximumLength(Country.NativeNameMaxLenght);
        RuleFor(x => x.Model.OfficialName).NotNull().MaximumLength(Country.OfficialNameMaxLenght);
        RuleFor(x => x.Model.Tld).MaximumLength(Country.TldMaxLenght);
        RuleFor(x => x.Model.Cca2).MaximumLength(Country.Cca2MaxLenght);
        RuleFor(x => x.Model.Cca3).MaximumLength(Country.Cca3MaxLenght);
        RuleFor(x => x.Model.Ccn3).MaximumLength(Country.Ccn3MaxLenght);
    }
}