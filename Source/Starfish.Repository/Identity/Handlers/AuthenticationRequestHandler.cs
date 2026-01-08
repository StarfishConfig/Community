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
			throw new BadRequestException("Username cannot be empty.");
		}

		if (string.IsNullOrWhiteSpace(message.Password))
		{
			throw new BadRequestException("Password cannot be empty.");
		}

		var specification = UserSpecification.UsernameEquals(message.Username);
		var predicate = specification.Satisfy();
		var user = await _context.Set<User>().FirstOrDefaultAsync(predicate, cancellationToken);
		if (user == null)
		{
			throw new AuthenticationException("Invalid username or password.");
		}

		var passwordHash = Cryptography.DES.Encrypt(message.Password, Encoding.UTF8.GetBytes(user.PasswordSalt));
		if (!string.Equals(passwordHash, user.PasswordHash, StringComparison.Ordinal))
		{
			throw new AuthenticationException("Invalid username or password.");
		}

		if (user.LockoutEnd > DateTime.UtcNow)
		{
			throw new AuthenticationException("User account is locked.");
		}

		var result = TypeAdapter.ProjectedAs<UserAuthQueryModel>(user);
		return result;
	}
}