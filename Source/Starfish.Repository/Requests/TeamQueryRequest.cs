using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

public record TeamDetailQuery(long Id) : IRequest<TeamDetailModel>;

public record TeamSearchQuery(string Keyword, bool? Owned, int Skip, int Size) : IRequest<IList<TeamListModel>>;

public record TeamCountQuery(string Keyword, bool? Owned) : IRequest<int>;

public record TeamMemberListQuery(long TeamId, string Keyword, int Skip, int Size) : IRequest<IList<TeamMemberModel>>;

public record TeamMemberCountQuery(long TeamId, string Keyword) : IRequest<int>;