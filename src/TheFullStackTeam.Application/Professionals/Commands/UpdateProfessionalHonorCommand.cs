using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Suited.ConnectionString.Resolver.Services.Contracts;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateProfessionalHonorCommand : IRequest<UpdateProfessionalHonorResult>
{
    public Guid ProfessionalId { get; }

    public Guid HonorId { get; }
    public HonorModel Model { get; set; }

    public UpdateProfessionalHonorCommand(HonorModel model, Guid id, Guid hId)
    {
        ProfessionalId = id;
        HonorId = hId;
        Model = model;
    }
}

/// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
public class UpdateProfessionalHonorCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalHonorCommand, UpdateProfessionalHonorResult>
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UpdateProfessionalHonorCommandHandler> _logger;

    public UpdateProfessionalHonorCommandHandler(TheFullStackTeamDbContext context, IConfiguration configuration, ILogger<UpdateProfessionalHonorCommandHandler> logger) : base(context)
    {
        _configuration = configuration;
        _logger = logger;

    }

    public async Task<UpdateProfessionalHonorResult> Handle(UpdateProfessionalHonorCommand request, CancellationToken cancellationToken)
    {
        var honor = await _context.Honors.AsNoTracking()
             .Where(h => h.ProfessionalId == request.ProfessionalId && h.Id == request.HonorId)
             .SingleOrDefaultAsync(cancellationToken);

        if (honor == null)
        {
            throw new NotFoundException(nameof(Honor), request.HonorId);
        }

        var organization = await _context.Organizations.Where(o => o.Id == request.Model.OrganizationId).SingleOrDefaultAsync(cancellationToken);

        var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (organization == null)
            {
                var imageUrl = _configuration["OraganizationNoImage"];
                organization = new Organization
                {
                    Name = request.Model.OrganizationName,
                    Logo = new ImageUrl { ThumbUrl = imageUrl, DisplayUrl = imageUrl }
                };
                await _context.Organizations.AddAsync(organization, cancellationToken);
            }

            honor.OrganizationName = request.Model.OrganizationName;
            honor.OrganizationId = organization.Id;
            honor.Title = request.Model.Title;
            honor.IssueDate = request.Model.IssueDate;
            honor.Description = request.Model.Description;

            _context.Honors.Update(honor);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return new UpdateProfessionalHonorResult(honor!);

        }

        catch (Exception e)
        {
            _logger.LogError(e, "Error on update Honor");
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}

/// <summary>
/// Result class
/// </summary>
public class UpdateProfessionalHonorResult : AppResult<HonorListItem>
{
    public UpdateProfessionalHonorResult(HonorListItem model) : base(model)
    {
    }
}

/// <summary>
/// Validate a command
/// </summary>
public sealed class UpdateProfessionalHonorCommandValidator : AbstractValidator<UpdateProfessionalHonorCommand>
{
    public UpdateProfessionalHonorCommandValidator(TheFullStackTeamDbContext context, ISessionService sessionService)
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