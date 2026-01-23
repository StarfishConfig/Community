using Duende.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class TokenQueryHandler : IHandler<TokenDetailQuery, TokenDetailModel>
{
	private readonly IdentityDataContext _context;

	public TokenQueryHandler(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<TokenDetailModel> HandleAsync(TokenDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var key = message.Token.ToSha256();
		var entity = await _context.Set<Token>()
		                           .AsNoTracking()
		                           .FirstOrDefaultAsync(t => t.Type == message.Type && t.Key == key, cancellationToken);
		if (entity == null)
		{
			return null;
		}

		return TypeAdapter.ProjectedAs<TokenDetailModel>(entity);
	}
}