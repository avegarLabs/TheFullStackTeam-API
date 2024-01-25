using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheFullStackTeam.Application.Model.EntityModel.Search;
using TheFullStackTeam.Application.Model.ListItem.Search;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;


namespace TheFullStackTeam.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly TheFullStackTeamDbContext _context;
        public SearchService(TheFullStackTeamDbContext context)
        {
            _context = context;

        }

        public async Task<SearchResultItem> SearchJobsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation)
        {
            var lowerJobTypes = criteriaModel.JobTypes.Select(jt => jt.ToLowerInvariant());
            var lowerContractTypes = criteriaModel.ContractTypes.Select(ct => ct.ToLowerInvariant());
            var lowerSkills = criteriaModel.Skills.Select(sk => sk.ToLower());
            var lowerLanguages = criteriaModel.LanguageList.Select(lang => lang.ToLowerInvariant());

            var skip = (criteriaModel.Pagination.Page - 1) * criteriaModel.Pagination.ItemsPerPage;

            IQueryable<Job> baseQuery = _context.Jobs
                .OrderByDescending(j => j.Created)
                .Include(j => j.JobSkills)
                .Include(j => j.JobContractTypes)
                .Include(j => j.JobsJobTypes)
                .Include(j => j.JobsSalaryTypes)
                .Include(j => j.RequiredLanguages).ThenInclude(l => l.Language);

            if (criteriaModel.CountryId != null)
                baseQuery = baseQuery.Where(j => j.CountryId.Equals(criteriaModel.CountryId));

            if (criteriaModel.ContractTypes.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobContractTypes.Any(jc => lowerContractTypes.Contains(jc.ContractTypeName.ToLowerInvariant())));

            if (criteriaModel.JobTypes.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobsJobTypes.Any(jj => lowerJobTypes.Contains(jj.JobTypeName.ToLowerInvariant())));

            if (criteriaModel.Skills.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobSkills.Any(s => lowerSkills.Contains(s.SkillName.ToLower())));

            if (criteriaModel.LanguageList.Count > 0)
                baseQuery = baseQuery.Where(j => j.RequiredLanguages.Any(l => lowerLanguages.Contains(l.Language.Name.ToLowerInvariant())));

            if (criteriaModel.SalaryHour)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("hour") && jst.MinAmount >= criteriaModel.HourSalaryMin && jst.MaxAmount <= criteriaModel.HourSalaryMax));

            if (criteriaModel.SalaryMonth)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("month") && jst.MinAmount >= criteriaModel.MonthSalaryMin && jst.MaxAmount <= criteriaModel.MonthSalaryMax));

            if (criteriaModel.SalaryYear)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("year") && jst.MinAmount >= criteriaModel.YearSalaryMin && jst.MaxAmount <= criteriaModel.YearSalaryMax));

            var results = await baseQuery
                .Select(item => new SearchResultListItem
                {
                    Id = item.Id,
                    Name = item.JobTitle,
                    Description = item.JobDescription,
                    Moniker = item.Moniker,
                    ItemURL = "/job/" + item.Moniker,
                    Picture = item.Professional == null ? item.Organization.Logo.DisplayUrl : item.Professional.User.Picture.DisplayUrl,
                    Owner = item.Professional == null ? item.Organization.Name : item.Professional.Name,
                    Specifications = string.Join(", ", item.JobsJobTypes.Select(jc => jc.JobTypeName)),
                    Type = item.Professional == null ? "org" : "prof",
                })
                .Skip(skip)
                .Take(criteriaModel.Pagination.ItemsPerPage)
                .AsNoTracking()
                .ToListAsync(cancellation);

            var totalItems = await CountJobsByCriteria(criteriaModel, cancellation);

            var resultQuery = new SearchResultItem()
            {
                TotalItems = totalItems,
                Results = results.ToList(),
                Page = criteriaModel.Pagination.Page

            };

            return resultQuery;
        }

        public async Task<SearchResultItem> SearchOrganizationAndProfessionalByName(string param, CancellationToken cancellation)
        {
            var professionalList = _context.Professionals
        .Where(p => p.Name.Contains(param))
        .AsNoTracking()
        .Select(x => new SearchResultListItem()
        {
            Id = x.Id,
            Name = x.Name,
            Moniker = x.Moniker,
            Description = x.Title,
            ItemURL = "/professional/" + x.Moniker,
            Owner = x.Name,
            Picture = x.Picture.DisplayUrl,
            Specifications = ""
        })
        .ToList();

            var organizationsList = _context.Organizations
                .Where(p => p.Name.Contains(param))
                .AsNoTracking()
                .Select(x => new SearchResultListItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Moniker = x.Moniker,
                    Description = x.Sector,
                    ItemURL = "/organization/" + x.Moniker,
                    Owner = x.Name,
                    Picture = x.Logo.DisplayUrl,
                    Specifications = ""
                })
                .ToList();


            var results = professionalList.Concat(organizationsList);
            var resultQuery = new SearchResultItem()
            {
                TotalItems = results.ToList().Count,
                Results = results.ToList()
            };

            return resultQuery;
        }

        public async Task<SearchResultProfilesItem> SearchProfessionalsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation)
        {
            var lowerJobTypes = criteriaModel.JobTypes.Select(jt => jt.ToLower()).ToList();
            var lowerContractTypes = criteriaModel.ContractTypes.Select(ct => ct.ToLower()).ToList();
            var lowerSkills = criteriaModel.Skills.Select(sk => sk.ToLower()).ToList();
            var lowerLanguages = criteriaModel.LanguageList.Select(lang => lang.ToLower()).ToList();

            var skip = (criteriaModel.Pagination.Page - 1) * criteriaModel.Pagination.ItemsPerPage;

            IQueryable<Professional> baseQuery = _context.Professionals
                .Include(p => p.ProfessionalSkills)
                .Include(p => p.ProfessionalContractTypes)
                .Include(p => p.ProfessionalJobTypes)
                .Include(p => p.ProfessionalSalaryTypes)
                .Include(p => p.ProfessionalLanguages).ThenInclude(l => l.Language);

            if (criteriaModel.CountryId != null)
                baseQuery = baseQuery.Where(p => p.CountryId.Equals(criteriaModel.CountryId));

            foreach (var ct in lowerContractTypes)
                baseQuery = baseQuery.Where(p => p.ProfessionalContractTypes.Any(pc => pc.Name.ToLower() == ct));

            foreach (var jt in lowerJobTypes)
                baseQuery = baseQuery.Where(p => p.ProfessionalJobTypes.Any(pj => pj.Name.ToLower() == jt));

            foreach (var sk in lowerSkills)
                baseQuery = baseQuery.Where(p => p.ProfessionalSkills.Any(s => s.SkillName.ToLower() == sk));

            foreach (var lang in lowerLanguages)
                baseQuery = baseQuery.Where(p => p.ProfessionalLanguages.Any(l => l.Language.Name.ToLower() == lang));

            if (criteriaModel.SalaryHour)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("hour") && pst.Amount >= criteriaModel.HourSalaryMin && pst.Amount <= criteriaModel.HourSalaryMax));

            if (criteriaModel.SalaryMonth)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("month") && pst.Amount >= criteriaModel.MonthSalaryMin && pst.Amount <= criteriaModel.MonthSalaryMax));

            if (criteriaModel.SalaryYear)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("year") && pst.Amount >= criteriaModel.YearSalaryMin && pst.Amount <= criteriaModel.YearSalaryMax));

            var professionalList = await baseQuery
                .OrderBy(p => p.Name)
                .Skip(skip)
                .Take(criteriaModel.Pagination.ItemsPerPage)
                .Select(item => new SearchResultProfilesListItem()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Title = item.Title,
                    Moniker = item.Moniker,
                    ItemURL = "/prof/" + item.Moniker,
                    Picture = item.Picture == null ? item.User.Picture.DisplayUrl : item.Picture.DisplayUrl,
                    Country = item.Country,
                    ServicesName = item.ProfessionalSevices.Select(ps => ps.ServiceName).ToList()
                })
                .AsNoTracking()
                .ToListAsync(cancellation);

            var totalItems = await CountProfessionalsByCriteria(criteriaModel, cancellation);

            var resultQuery = new SearchResultProfilesItem()
            {
                TotalItems = totalItems,
                Results = professionalList.ToList(),
                Page = criteriaModel.Pagination.Page
            };

            return resultQuery;
        }
    public async Task<SearchResultServicesItem> SearchServiceCategoriesByCriteria(SearchServiceCriteriaModel criteriaModel, CancellationToken cancellation)
         {
             var lowerSkills = criteriaModel.Skills.Select(sk => sk.ToLower()).ToList();
             var lowerCategories = criteriaModel.Categoties.Select(lang => lang.ToLower()).ToList();

             var skip = (criteriaModel.Pagination.Page - 1) * criteriaModel.Pagination.ItemsPerPage;

             var professionalServices = await _context.ProfessionalSevices.OrderByDescending(ps => ps.Created)
            .Include(ps => ps.ProfessionalServiceCategories)
            .Where(ps => (criteriaModel.Categoties.Count == 0 || ps.ProfessionalServiceCategories.Any(c => lowerCategories.Contains(c.Category.Name.ToLower()))) &&
                        (criteriaModel.Skills.Count == 0 || ps.ServiceSkills.Any(s => lowerSkills.Contains(s.Name.ToLower()))) &&
                        (criteriaModel.ServicePriceMin == 0 || ps.SevicePrice >= criteriaModel.ServicePriceMin && ps.SevicePrice <= criteriaModel.ServicePriceMax) &&
                        (criteriaModel.ServicePriceMax == 0 || ps.SevicePrice <= criteriaModel.ServicePriceMax && ps.SevicePrice >= criteriaModel.ServicePriceMin)
            ).Select(item => new SearchResultServicesListItem()
            {
                Id = item.Id,
                Title = item.ServiceName,
                Description = item.ServiceDescription,
                Owner = item.Professional.Name,
                Currency = item.Currency,
                Price = item.SevicePrice,
                Moniker = item.Moniker,
                Skills = item.ServiceSkills.Select(ss => ss.Name).ToList(),
                Categories = item.ProfessionalServiceCategories.Select(psc => psc.Category.Name).ToList()

            }).Skip(skip).Take(criteriaModel.Pagination.ItemsPerPage).AsNoTracking().ToListAsync(cancellation);

             var organizationServices = await _context.OrganizationSevices.OrderByDescending(ps => ps.Created)
              .Include(os => os.OrganizationServiceCategories)
              .Where(os => (criteriaModel.Categoties.Count == 0 || os.OrganizationServiceCategories.Any(c => lowerCategories.Contains(c.Category.Name.ToLower()))) &&
                          (criteriaModel.Skills.Count == 0 || os.ServiceSkills.Any(s => lowerSkills.Contains(s.Name.ToLower()))) &&
                          (criteriaModel.ServicePriceMin == 0 || os.SevicePrice >= criteriaModel.ServicePriceMin && os.SevicePrice <= criteriaModel.ServicePriceMax) &&
                          (criteriaModel.ServicePriceMax == 0 || os.SevicePrice <= criteriaModel.ServicePriceMax && os.SevicePrice >= criteriaModel.ServicePriceMin)
              ).Select(item => new SearchResultServicesListItem()
              {
                  Id = item.Id,
                  Title = item.ServiceName,
                  Description = item.ServiceDescription,
                  Owner = item.Organization.Name,
                  Currency = item.Currency,
                  Price = item.SevicePrice,
                  Moniker = item.Moniker,
                  Skills = item.ServiceSkills.Select(ss => ss.Name).ToList(),
                  Categories = item.OrganizationServiceCategories.Select(psc => psc.Category.Name).ToList()

              }).Skip(skip).Take(criteriaModel.Pagination.ItemsPerPage).AsNoTracking().ToListAsync(cancellation);

             var resultQuery = new SearchResultServicesItem()
             {
                 TotalItems = professionalServices.Concat(organizationServices).Count(),
                 Results = professionalServices.Concat(organizationServices).ToList(),
                 Page = criteriaModel.Pagination.Page,
             };

             return resultQuery;

         }


        public async Task<int> CountProfessionalsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation)
        {
            var lowerJobTypes = criteriaModel.JobTypes.Select(jt => jt.ToLower()).ToList();
            var lowerContractTypes = criteriaModel.ContractTypes.Select(ct => ct.ToLower()).ToList();
            var lowerSkills = criteriaModel.Skills.Select(sk => sk.ToLower()).ToList();
            var lowerLanguages = criteriaModel.LanguageList.Select(lang => lang.ToLower()).ToList();

            var skip = (criteriaModel.Pagination.Page - 1) * criteriaModel.Pagination.ItemsPerPage;

            IQueryable<Professional> baseQuery = _context.Professionals
                .Include(p => p.ProfessionalSkills)
                .Include(p => p.ProfessionalContractTypes)
                .Include(p => p.ProfessionalJobTypes)
                .Include(p => p.ProfessionalSalaryTypes)
                .Include(p => p.ProfessionalLanguages).ThenInclude(l => l.Language);

            if (criteriaModel.CountryId != null)
                baseQuery = baseQuery.Where(p => p.CountryId.Equals(criteriaModel.CountryId));

            foreach (var ct in lowerContractTypes)
                baseQuery = baseQuery.Where(p => p.ProfessionalContractTypes.Any(pc => pc.Name.ToLower() == ct));

            foreach (var jt in lowerJobTypes)
                baseQuery = baseQuery.Where(p => p.ProfessionalJobTypes.Any(pj => pj.Name.ToLower() == jt));

            foreach (var sk in lowerSkills)
                baseQuery = baseQuery.Where(p => p.ProfessionalSkills.Any(s => s.SkillName.ToLower() == sk));

            foreach (var lang in lowerLanguages)
                baseQuery = baseQuery.Where(p => p.ProfessionalLanguages.Any(l => l.Language.Name.ToLower() == lang));

            if (criteriaModel.SalaryHour)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("hour") && pst.Amount >= criteriaModel.HourSalaryMin && pst.Amount <= criteriaModel.HourSalaryMax));

            if (criteriaModel.SalaryMonth)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("month") && pst.Amount >= criteriaModel.MonthSalaryMin && pst.Amount <= criteriaModel.MonthSalaryMax));

            if (criteriaModel.SalaryYear)
                baseQuery = baseQuery.Where(p => p.ProfessionalSalaryTypes.Any(pst => pst.PaymentPeriod.Equals("year") && pst.Amount >= criteriaModel.YearSalaryMin && pst.Amount <= criteriaModel.YearSalaryMax));

            return await baseQuery
               .Select(item => new SearchResultListItem
               {
                   Id = item.Id

               }).CountAsync(cancellation);
        }

        public async Task<int> CountJobsByCriteria(SearchCriteriaModel criteriaModel, CancellationToken cancellation)
        {
            var lowerJobTypes = criteriaModel.JobTypes.Select(jt => jt.ToLowerInvariant());
            var lowerContractTypes = criteriaModel.ContractTypes.Select(ct => ct.ToLowerInvariant());
            var lowerSkills = criteriaModel.Skills.Select(sk => sk.ToLower());
            var lowerLanguages = criteriaModel.LanguageList.Select(lang => lang.ToLowerInvariant());

            var skip = (criteriaModel.Pagination.Page - 1) * criteriaModel.Pagination.ItemsPerPage;

            IQueryable<Job> baseQuery = _context.Jobs
                .Include(j => j.JobSkills)
                .Include(j => j.JobContractTypes)
                .Include(j => j.JobsJobTypes)
                .Include(j => j.JobsSalaryTypes)
                .Include(j => j.RequiredLanguages).ThenInclude(l => l.Language);

            if (criteriaModel.CountryId != null)
                baseQuery = baseQuery.Where(j => j.CountryId.Equals(criteriaModel.CountryId));

            if (criteriaModel.ContractTypes.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobContractTypes.Any(jc => lowerContractTypes.Contains(jc.ContractTypeName.ToLowerInvariant())));

            if (criteriaModel.JobTypes.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobsJobTypes.Any(jj => lowerJobTypes.Contains(jj.JobTypeName.ToLowerInvariant())));

            if (criteriaModel.Skills.Count > 0)
                baseQuery = baseQuery.Where(j => j.JobSkills.Any(s => lowerSkills.Contains(s.SkillName.ToLower())));

            if (criteriaModel.LanguageList.Count > 0)
                baseQuery = baseQuery.Where(j => j.RequiredLanguages.Any(l => lowerLanguages.Contains(l.Language.Name.ToLowerInvariant())));

            if (criteriaModel.SalaryHour)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("hour") && jst.MinAmount >= criteriaModel.HourSalaryMin && jst.MaxAmount <= criteriaModel.HourSalaryMax));

            if (criteriaModel.SalaryMonth)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("month") && jst.MinAmount >= criteriaModel.MonthSalaryMin && jst.MaxAmount <= criteriaModel.MonthSalaryMax));

            if (criteriaModel.SalaryYear)
                baseQuery = baseQuery.Where(j => j.JobsSalaryTypes.Any(jst => jst.SalaryTypeName.Equals("year") && jst.MinAmount >= criteriaModel.YearSalaryMin && jst.MaxAmount <= criteriaModel.YearSalaryMax));

            return await baseQuery
               .Select(item => new SearchResultListItem
               {
                   Id = item.Id

               }).CountAsync(cancellation);
        }
    }
}

