using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class OnetimePasswordSpecification
{
	public static Specification<OnetimePassword> RequestIdEquals(string requestId)
	{
		requestId = requestId.Normalize(TextCaseType.Lower);
		return new DirectSpecification<OnetimePassword>(t => t.RequestId == requestId);
	}

	public static Specification<OnetimePassword> UsageEquals(OnetimePasswordUsage usage)
	{
		return new DirectSpecification<OnetimePassword>(t => t.Usage == usage);
	}

	public static Specification<OnetimePassword> Available()
	{
		return new DirectSpecification<OnetimePassword>(t => t.Checked == null && (t.Expiration == null || t.Expiration > DateTime.UtcNow));
	}

	public static Specification<OnetimePassword> Unavailable()
	{
		return new DirectSpecification<OnetimePassword>(t => t.Checked != null || (t.Expiration != null && t.Expiration <= DateTime.UtcNow));
	}

	public static Specification<OnetimePassword> RecipientEquals(string recipient)
	{
		recipient = recipient.Normalize(TextCaseType.Lower);
		return new DirectSpecification<OnetimePassword>(t => t.Recipient == recipient);
	}
}
