using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Services.Contracts;
using TheFullStackTeam.Application.Users.Command;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class DeleteUserProfileCommandHandler : AppRequestHandler, IRequestHandler<DeleteUserProfileCommand, DeleteCommandResult>
    {
        private readonly IUserService _userService;
        public DeleteUserProfileCommandHandler(TheFullStackTeamDbContext context, IUserService service) : base(context)
        {
            _userService = service;
        }

        public async Task<DeleteCommandResult> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Where(u => u.Id == request.UserId).SingleOrDefault();
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            await CleanUserRelations(user);
            _userService.DeleteUser(user.ContactEmail);
            var roles = _context.UserRole.Where(u => u.UserId.Equals(user.Id)).AsNoTracking().ToList();
            _context.UserRole.RemoveRange(roles);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteCommandResult(true);

        }

        private Task CleanUserRelations(User user)
        {
            var prof = _context.Professionals.Where(p => p.UserId.Equals(user.Id)).SingleOrDefault();
            if (prof != null)
            {
                _context.Professionals.Remove(prof);
            }

            var organizations = _context.Organizations.Where(o => o.UserId.Equals(user.Id)).ToList();
            if (organizations != null)
            {
                _context.Organizations.RemoveRange(organizations);
            }

            return Task.CompletedTask;

        }
    }
}
