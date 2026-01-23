using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

public record TokenDetailQuery(string Type, string Token) : IRequest<TokenDetailModel>;