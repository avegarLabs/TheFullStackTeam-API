using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.ValueObjects;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class UploadUserAvatarCommandHandler : AppRequestHandler, IRequestHandler<UploadUserAvatarCommand, ReadUserProfileInformationResult>
    {

        private readonly IStorageService _storageService;
        public UploadUserAvatarCommandHandler(TheFullStackTeamDbContext context, IStorageService service) : base(context)
        {
            _storageService = service;
        }

        public async Task<ReadUserProfileInformationResult> Handle(UploadUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(u => u.AccountId.Equals(request.AccountId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(Users), request.AccountId);
            }

            var avatar = await _storageService.StoreUserProfileImage(request.Avatar.Base64File, request.Avatar.FileName, user.Moniker);
            user.Picture = new ImageUrl
            {
                DisplayUrl = avatar.displayImageUri.ToString(),
                ThumbUrl = avatar.thumbImageUri.ToString()
            };

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ReadUserProfileInformationResult(user);

        }
    }
}
