using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.CvPdfGenerator;
using TheFullStackTeam.CvPdfGenerator.ViewModels;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers
{
    public class GetProfessionalCvPdfHandler : AppRequestHandler, IRequestHandler<GetProfessionalCvPdfQuery, GetProfessionalCvPDFResults>
    {
        private readonly IHtmlToPdf _htmlToPdf;
        private readonly ITemplateService _templateService;
        private IStorageService _storage;

        public GetProfessionalCvPdfHandler(TheFullStackTeamDbContext context, IHtmlToPdf htmlToPdf, ITemplateService template, IStorageService storage) : base(context)
        {
            _htmlToPdf = htmlToPdf;
            _templateService = template;
            _storage = storage;
        }

        public async Task<GetProfessionalCvPDFResults> Handle(GetProfessionalCvPdfQuery request, CancellationToken cancellationToken)
        {
            var professional = await _context.Professionals.Where(p => p.Id.Equals(request.ProfessionalId)).SingleOrDefaultAsync(cancellationToken);
            if(professional == null)
            {
                throw new NotFoundException(nameof(Professional), request.ProfessionalId);
            }


            if (professional.VitaePath == null)
            {

                var idDoc = professional.GetHashCode();
                var professionalModel = new ProfessionalVitaeViewModel()
                {

                    Id = idDoc,
                    Name = professional.Name,
                    ContactEmail = professional.ContactEmail,
                    Phone = professional.Phone,
                    PersonalWeb = professional.PersonalWeb,
                    Image = professional.Picture != null ? professional.Picture.DisplayUrl : string.Empty,
                    AboutMe = professional.AboutMe,
                    CountryName = professional.Country.CommonName,
                    Title = professional.Title,
                    skillListItems = professional.ProfessionalSkills.AsQueryable().Select(ProfessionalSkillListItem.Projection).ToList(),
                    positionListItems = professional.Experiences.AsQueryable().Select(PositionListItem.Projection).ToList(),
                    titleListItems = professional.Titles.AsQueryable().Select(TitleListItem.Projection).ToList(),
                    professionalLanguegeListItems = professional.ProfessionalLanguages.AsQueryable().Select(ProfessionalLanguegeListItem.Projection).ToList()
                };

                var pdfTemplate = await _templateService.RenderViewAsync("VitaeView", professionalModel);
                string pdfEncode = await _htmlToPdf.WritePdf(pdfTemplate, professional.Moniker, $"ID n_{idDoc.ToString()}");

                var pdfPath = await _storage.StoreProfessionalVitaeFile(pdfEncode, $"{professional.Moniker}.pdf", professional.Moniker);
                professional.VitaePath = pdfPath.displayImageUri.ToString();
                professional.VitaeId = idDoc.ToString();

                _context.Professionals.Update(professional);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new GetProfessionalCvPDFResults(professional.VitaePath);
        
        }
    }
}
