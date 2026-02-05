using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Requests;

public record ConfigurationBaseInfoQuery(IList<long> TeamScope, long Id) : IRequest<ConfigurationBaseInfoModel>;

public record ConfigurationSearchQuery(IList<long> TeamScope, string Keyword, long TeamId, int Skip, int Size) : IRequest<IList<ConfigurationListModel>>;

public record ConfigurationCountQuery(IList<long> TeamScope, string Keyword, long TeamId) : IRequest<int>;

public record ConfigurationDetailQuery(IList<long> TeamScope, long Id) : IRequest<ConfigurationDetailModel>;

public record ConfigurationPermissionGrantQuery(IList<long> TeamScope, long ConfigurationId, string UserId) : IRequest<IReadOnlyDictionary<long, ConfigurationPermissionGrantState>>;