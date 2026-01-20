using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

public record TeamDetailQuery(long Id) : IRequest<TeamDetailModel>;

public record TeamSearchQuery(string Keyword, int? Type, int Skip, int Size) : IRequest<IList<TeamListModel>>;

public record TeamCountQuery(string Keyword, int? Type) : IRequest<int>;

public record TeamMemberQuery(long TeamId, string Keyword, int Skip, int Size) : IRequest<IList<TeamMemberModel>>;