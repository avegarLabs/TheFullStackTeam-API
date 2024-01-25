using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Communications.EmailTemplates.ViewModels;
using TheFullStackTeam.Communications.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Commands;

public class ProfessionalRegisterCommand : IRequest<ProfessionalSignUpCommandResult>
{
    public RegisterProfessionalModel Model { get; set; }
    public ProfessionalRegisterCommand(RegisterProfessionalModel model)
    {
        Model = model;
    }
}

public class ProfessionalSignUpCommandHandler : AppRequestHandler,
    IRequestHandler<ProfessionalRegisterCommand, ProfessionalSignUpCommandResult>
{
    private readonly IMonikerService _monikerService;
    private readonly IEmailService _emailService;

    public ProfessionalSignUpCommandHandler(TheFullStackTeamDbContext context, IMonikerService monikerService, IEmailService email) :
        base(context)
    {
        _monikerService = monikerService;
        _emailService = email;

    }

    public async Task<ProfessionalSignUpCommandResult> Handle(ProfessionalRegisterCommand request,
        CancellationToken cancellationToken)
    {
        var professionalMoniker = await _monikerService.FindValidMoniker<Professional>(request.Model.Name);
        var user = await _context.Users.Where(u => u.AccountId.Equals(request.Model.AccountId)).SingleOrDefaultAsync(cancellationToken);
        var professional = new Professional()
        {
            Moniker = professionalMoniker,
            Name = request.Model.Name,
            AboutMe = request.Model.AboutMe,
            Title = request.Model.Title,
            UserId = user.Id,
            CountryId = user.Country.Id,
            Phone = user.Phone,
            ContactEmail = user.ContactEmail,
            LinkedInProfile = "",
            PersonalWeb = "",
            YoutubeProfile = "",
            Industry = request.Model.Industry,
        };
        await _context.Professionals.AddAsync(professional, cancellationToken);
        await CreateSkillByProfessional(professional, request.Model.Skills, cancellationToken);
        await sentEmailToProfessional(user, professional);
        await _context.SaveChangesAsync(cancellationToken);
        return new ProfessionalSignUpCommandResult(user.Professional);
    }

    private Task sentEmailToProfessional(User user, Professional entity)
    {
        var dataModel = new ProfessionalCreatedNotificationViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = entity.Name,
            Email = entity.ContactEmail,
            Moniker = entity.Moniker,
            Moto = entity.Title
        };
        _emailService.SendProfessionalWelcomeEmail(dataModel, entity.ContactEmail);
        return Task.CompletedTask;
    }


    private async Task<bool> CreateSkillByProfessional(Professional professional, IEnumerable<ProfessionalSkillModel> skills, CancellationToken cancellationToken)
    {
        foreach (ProfessionalSkillModel skillModel in skills)
        {
            var item = await _context.Skills.Where(s => s.Name.Equals(skillModel.Name)).SingleOrDefaultAsync(cancellationToken);
            var professionalSkill = new ProfessionalSkill()
            {
                SkillName = item.Name,
                ProfessionalId = professional.Id,
                SkillId = item.Id,
                SkillLevel = skillModel.Level,
            };
            _context.ProfessionalSkills.Add(professionalSkill);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public class ProfessionalSignUpCommandResult : AppResult<ProfessionalListItem>
{
    public ProfessionalSignUpCommandResult(ProfessionalListItem model) : base(model)
    {
    }
}