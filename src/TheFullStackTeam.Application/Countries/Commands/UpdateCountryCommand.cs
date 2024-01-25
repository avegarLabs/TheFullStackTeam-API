using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Model.PUT;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Countries.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateCountryCommand : IRequest<UpdateCountryCommandResult>
{
    public UpdateCountryCommand(Guid id, CountryListItem model)
    {
        Id = id;
        Model = model;
    }

    public Guid Id { get; }
    public CountryListItem Model { get; }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, UpdateCountryCommandResult>
{
    private readonly TheFullStackTeamDbContext _context;

    public UpdateCountryCommandHandler(TheFullStackTeamDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateCountryCommandResult> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        entity.CommonName = request.Model.CommonName;
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCountryCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class UpdateCountryCommandResult : AppResult<CountryListItem>
{
    public UpdateCountryCommandResult(CountryListItem model) : base(model)
    {
    }
}

/// <summary>
/// Fluent update country command validataor 
/// </summary>
public sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Countries.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");

        RuleFor(x => x.Model.CommonName).MaximumLength(Country.CommonNameMaxLenght);
        RuleFor(x => x.Model.NativeName).MaximumLength(Country.NativeNameMaxLenght);
        RuleFor(x => x.Model.OfficialName).MaximumLength(Country.OfficialNameMaxLenght);
        RuleFor(x => x.Model.Tld).MaximumLength(Country.TldMaxLenght);
        RuleFor(x => x.Model.Cca2).MaximumLength(Country.Cca2MaxLenght);
        RuleFor(x => x.Model.Cca3).MaximumLength(Country.Cca3MaxLenght);
        RuleFor(x => x.Model.Ccn3).MaximumLength(Country.Ccn3MaxLenght);
    }
}