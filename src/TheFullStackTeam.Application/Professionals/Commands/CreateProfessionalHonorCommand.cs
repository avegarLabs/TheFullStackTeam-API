using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalHonorCommand : IRequest<CreateProfessionalHonorCommandResult>
{
    public Guid ProfessionalId { get; }
    public HonorModel Model { get; set; }
    public CreateProfessionalHonorCommand(HonorModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class CreateProfessionalHonorCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalHonorCommand, CreateProfessionalHonorCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly IConfiguration _configuration;

    public CreateProfessionalHonorCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, IConfiguration configuration) : base(context)
    {
        _monikerService = monikerService;
        _configuration = configuration;
    }

    public async Task<CreateProfessionalHonorCommandResult> Handle(CreateProfessionalHonorCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
        if (professional == null)
        {
            throw new NotFoundException(nameof(Professional), request.ProfessionalId);
        }


        Honor entity = request.Model;
        entity.ProfessionalId = professional.Id;

        var organization = await _context.Organizations.Where(o => o.Id == request.Model.OrganizationId).SingleOrDefaultAsync(cancellationToken);

        if (organization == null)
        {
            var imageUrl = _configuration["OraganizationNoImage"];
            organization = new Organization()
            {
                Name = request.Model.OrganizationName,
                Logo = new ImageUrl { ThumbUrl = imageUrl, DisplayUrl = imageUrl },
                Honors = new List<Honor> { entity }
            };
            await _context.Organizations.AddAsync(organization);
        }
        else
        {
            organization.Honors.Add(entity);
            _context.Organizations.Update(organization);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProfessionalHonorCommandResult(entity);
    }
}

/// <inheritdoc cref="AppResult{TModel}"/>
public class CreateProfessionalHonorCommandResult : AppResult<HonorListItem>
{
    public CreateProfessionalHonorCommandResult(HonorListItem model) : base(model)
    {
    }
}

public sealed class CreateProfessionalHonorCommandValidator : AbstractValidator<CreateProfessionalHonorCommand>
{
    public CreateProfessionalHonorCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
    {
        RuleFor(r => r.ProfessionalId).Must(m => context.Professionals
                .Any(professional => professional.Id == m &&
                                     (professional.User.AccountId == sessionService.AccountId() || sessionService.IsAdmin())))
            .WithMessage(_ => "You not have a permission to make this operation");

        RuleFor(cmd => cmd.Model.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(cmd => cmd.Model.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(cmd => cmd.Model.Title).MaximumLength(Honor.TitleMaxLenght);
        RuleFor(cmd => cmd.Model.Description).MaximumLength(Honor.DescriptionMaxLenght);

        RuleFor(cmd => cmd.Model.IssueDate).Must(date => date.Date < DateTime.UtcNow.Date)
            .WithMessage("Date must be greater than today");
    }
}