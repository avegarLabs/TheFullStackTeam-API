using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.OrganizationServices
{
    internal class CreateOrganizationServicesTypeCommandHandler : AppRequestHandler, IRequestHandler<CreateOrganizationServicesTypeCommand, CreatedOrganizationServicesCommandResult>
    {
        private readonly IMonikerService _imoniker;
        public CreateOrganizationServicesTypeCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _imoniker = moniker;
        }

        public async Task<CreatedOrganizationServicesCommandResult> Handle(CreateOrganizationServicesTypeCommand request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations.Where(p => p.Id == request.OrganizationId).SingleOrDefaultAsync(cancellationToken);

            if (organization == null)
            {
                throw new NotFoundException(nameof(Organizations), request.OrganizationId);
            }

            var organizationServices = new OrganizationSevices()
            {
                Moniker = await _imoniker.FindValidMoniker<OrganizationSevices>(request.Model.ServicesName),
                ServiceName = request.Model.ServicesName,
                ServiceDescription = request.Model.ServiceDescription,
                SevicePrice = request.Model.Price,
                Currency = request.Model.Currency,
                OrganizationId = organization.Id
            };
            
            if (request.Model.SkillList.Count()>0)
            {
                organizationServices.ServiceSkills =  await AddSkillToService(request.Model.SkillList, cancellationToken);
            }

            await _context.OrganizationSevices.AddAsync(organizationServices);

            if (request.Model.CategoryList.Count() > 0)
            {
               await AddCategoriesToService(organizationServices, request.Model.CategoryList, cancellationToken);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return new CreatedOrganizationServicesCommandResult(organizationServices);
        }

        private async Task AddCategoriesToService(OrganizationSevices organizationServices, List<string> categoryList, CancellationToken cancellationToken)
        {
            List<OrganizationServiceCategory> categories= new List<OrganizationServiceCategory>();
            foreach (string category in categoryList)
            {
                var item = await _context.Categories.Where(c => c.Name.Equals(category)).SingleOrDefaultAsync(cancellationToken);
                var catServ = new OrganizationServiceCategory()
                {
                    CategoryId = item.Id,
                    OrganizationSevicesId = organizationServices.Id
                };
                categories.Add(catServ);
            }
            await _context.OrganizationServiceCategories.AddRangeAsync(categories);
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
