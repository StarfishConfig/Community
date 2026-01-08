using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class AuthenticationRequestHandler : IHandler<AuthenticateWithUsernameRequest, UserAuthQueryModel>
{
	private readonly IdentityDataContext _context;

	public AuthenticationRequestHandler(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<UserAuthQueryModel> HandleAsync(AuthenticateWithUsernameRequest message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(message.Username))
		{
			throw new BadRequestException(IdentityResources.IDS_ERROR_USERNAME_REQUIRED);
		}

		if (string.IsNullOrWhiteSpace(message.Password))
		{
			throw new BadRequestException(IdentityResources.IDS_ERROR_PASSWORD_REQUIRED);
		}

		var specification = UserSpecification.UsernameEquals(message.Username);
		var predicate = specification.Satisfy();
		var user = await _context.Set<User>().FirstOrDefaultAsync(predicate, cancellationToken);
		if (user == null)
		{
			throw new AuthenticationException(IdentityResources.IDS_ERROR_INVALID_USERNAME_PASSWORD);
		}

		var passwordHash = Cryptography.DES.Encrypt(message.Password, Encoding.UTF8.GetBytes(user.PasswordSalt));
		if (!string.Equals(passwordHash, user.PasswordHash, StringComparison.Ordinal))
		{
			throw new AuthenticationException(IdentityResources.IDS_ERROR_INVALID_USERNAME_PASSWORD);
		}

		if (user.LockoutEnd > DateTime.UtcNow)
		{
			throw new AuthenticationException(IdentityResources.IDS_ERROR_USER_LOCKED);
		}

		var result = TypeAdapter.ProjectedAs<UserAuthQueryModel>(user);
		return result;
	}
}