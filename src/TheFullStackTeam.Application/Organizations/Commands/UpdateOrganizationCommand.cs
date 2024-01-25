using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateOrganizationCommand : IRequest<UpdateOrganizationCommandResult>
{
    public Guid Id { get; }
    public OrganizationModel Model { get; }

    public UpdateOrganizationCommand(Guid organizationId, OrganizationModel model)
    {
        Id = organizationId;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateOrganizationCommandHandler : AppRequestHandler, IRequestHandler<UpdateOrganizationCommand, UpdateOrganizationCommandResult>
{
   public UpdateOrganizationCommandHandler(TheFullStackTeamDbContext context) : base(context)
    {
      
    }

    public async Task<UpdateOrganizationCommandResult> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Organizations.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new Exception("Organization not found");
        }

        entity.Name = request.Model.Name;
        entity.Description = request.Model.Description;
        entity.Sector = request.Model.Sector;
        entity.Phone = request.Model.Phone;
        entity.LinkedInProfile = request.Model.LinkedInProfile;
        entity.Description = request.Model.Description;  
        entity.ContactEmail = request.Model.ContactEmail;
        entity.YoutubeProfile = request.Model.YoutubeProfile;
        entity.OrganizationWeb = request.Model.OrganizationWeb;
        entity.Zise = request.Model.Zise;
        entity.CountryId = request.Model.Country.Id;
         
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateOrganizationCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class UpdateOrganizationCommandResult : AppResult<OrganizationListItem>
{
    public UpdateOrganizationCommandResult(OrganizationListItem model) : base(model)
    {
    }
}

/// <summary>
/// Fluent update organization command validataor 
/// </summary>
public sealed class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator(TheFullStackTeamDbContext context)
    {
        RuleFor(x => x.Id).Must(id => context.Organizations.Any(a => a.Id == id))
            .WithMessage(m => $"Not found entity with this identifier: {m.Id}");
    }
}