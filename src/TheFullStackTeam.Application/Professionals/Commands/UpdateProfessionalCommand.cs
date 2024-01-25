using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateProfessionalCommand : IRequest<UpdateProfessionalCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalListItem Model { get; }

    public UpdateProfessionalCommand(Guid id, ProfessionalListItem model)
    {
        ProfessionalId = id;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateProfessionalCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalCommand, UpdateProfessionalCommandResult>
{

    public UpdateProfessionalCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
    }

    public async Task<UpdateProfessionalCommandResult> Handle(UpdateProfessionalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }

        entity.Name = request.Model.Name;
        entity.Title = request.Model.Title;
        entity.AboutMe = request.Model.AboutMe;
        entity.Phone = request.Model.Phone;
        entity.ContactEmail = request.Model.ContactEmail;
        entity.PersonalWeb = request.Model.PersonalWeb;
        entity.LinkedInProfile = request.Model.LinkedInProfile;
        entity.YoutubeProfile = request.Model.YoutubeProfile;
        entity.Industry = request.Model.Industry;
        entity.CountryId = request.Model.Country.Id;

        _context.Professionals.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProfessionalCommandResult(entity);
    }
}

/// <summary>
/// Result class
/// </summary>
public class UpdateProfessionalCommandResult : AppResult<ProfessionalListItem>
{
    public UpdateProfessionalCommandResult(ProfessionalListItem model) : base(model)
    {
    }
}

/// <summary>
/// Update validator
/// </summary>
public sealed class UpdateProfessionalCommandValidator : AbstractValidator<UpdateProfessionalCommand>
{
    public UpdateProfessionalCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(x => x.Model.AboutMe).NotEmpty().MaximumLength(Professional.AboutMeMaxLenght);
        RuleFor(x => x.Model.Title).NotEmpty().MaximumLength(Professional.HeadLineMaxLenght);
    }
}