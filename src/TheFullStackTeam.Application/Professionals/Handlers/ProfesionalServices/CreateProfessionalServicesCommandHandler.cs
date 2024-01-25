using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    internal class CreateProfessionalServicesTypeCommandHandler : AppRequestHandler, IRequestHandler<CreateProfessionalServicesTypeCommand, CreatedProfessionalServicesCommandResult>
    {

        private readonly IMonikerService _imoniker;
        public CreateProfessionalServicesTypeCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _imoniker = moniker;
        }

        public async Task<CreatedProfessionalServicesCommandResult> Handle(CreateProfessionalServicesTypeCommand request, CancellationToken cancellationToken)
        {
            var professional = await _context.Professionals.Where(p => p.Id == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);

            if (professional == null)
            {
                throw new NotFoundException(nameof(Professional), professional.Moniker);
            }

            var professionalServices = new ProfessionalSevices()
            {
                Moniker = await _imoniker.FindValidMoniker<ProfessionalSevices>(request.Model.ServicesName),
                ServiceName = request.Model.ServicesName,
                ServiceDescription = request.Model.ServiceDescription,
                SevicePrice = request.Model.Price,
                Currency = request.Model.Currency,
                ProfessionalId = professional.Id
            };
            if (request.Model.SkillList.Count() > 0)
            {
                professionalServices.ServiceSkills = await AddSkillToService(request.Model.SkillList, cancellationToken);
            }
            await _context.ProfessionalSevices.AddAsync(professionalServices);
            if (request.Model.CategoryList.Count()>0)
            {
                await AddCategoriesToService(professionalServices, request.Model.CategoryList, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new CreatedProfessionalServicesCommandResult(professionalServices);
        }

        private async Task AddCategoriesToService(ProfessionalSevices professionalSevices, List<string> categoryList, CancellationToken cancellationToken)
        {
            List<ProfessionalServiceCategory> categories = new List<ProfessionalServiceCategory>();
            foreach (string category in categoryList)
            {
                var item = await _context.Categories.Where(c => c.Name.Equals(category)).SingleOrDefaultAsync(cancellationToken);
                var catServ = new ProfessionalServiceCategory()
                {
                    CategoryId = item.Id,
                    ProfessionalServiceId = professionalSevices.Id
                };
                categories.Add(catServ);
            }
            await _context.ProfessionalServiceCategories.AddRangeAsync(categories);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<List<Skill>> AddSkillToService(IEnumerable<string> skillVersionsModels, CancellationToken cancellationToken)
        {
            List<Skill> tempList = new List<Skill>();

            foreach (var item in skillVersionsModels)
            {
                var skill = await _context.Skills
                   .Where(s => s.Moniker.ToLower().Equals(item.ToLower().Trim()))
                   .SingleOrDefaultAsync(cancellationToken);

                if (skill == null)
                {
                    skill = new Skill()
                    {
                        Moniker = await _imoniker.FindValidMoniker<Skill>(item.Trim()),
                        Name = item
                    };
                    await _context.Skills.AddAsync(skill, cancellationToken);
                }
               tempList.Add(skill);
            }
            return tempList;
        }
    }

}
