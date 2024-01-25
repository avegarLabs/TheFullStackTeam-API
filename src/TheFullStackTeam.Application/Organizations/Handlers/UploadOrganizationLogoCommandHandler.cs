using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Organizations.Command;
using TheFullStackTeam.Application.Organizations.Results;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handler
{
    public class UploadOrganizationLogoCommandHandler : AppRequestHandler, IRequestHandler<UploadOrganizationLogoCommand, ReadOrganizationDetailsResults>
    {

        private readonly IStorageService _storageService;
        public UploadOrganizationLogoCommandHandler(TheFullStackTeamDbContext context, IStorageService service) : base(context)
        {
            _storageService = service;
        }

        public async Task<ReadOrganizationDetailsResults> Handle(UploadOrganizationLogoCommand request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations.Where(o => o.Id == request.OrganizationId ).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (organization == null)
            {
                throw new NotFoundException(nameof(Organizations), request.OrganizationId);
            }

            var avatar = await _storageService.StoreOrganizationLogo(request.Avatar.Base64File, request.Avatar.FileName, organization.Moniker);
            organization.Logo = new ImageUrl
            {
                DisplayUrl = avatar.displayImageUri.ToString(),
                ThumbUrl = avatar.thumbImageUri.ToString()
            };

            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync(cancellationToken);

            return new ReadOrganizationDetailsResults(organization);

        }
    }
}
