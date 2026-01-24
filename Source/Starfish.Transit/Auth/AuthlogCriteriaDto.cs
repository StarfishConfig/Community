namespace Nerosoft.Starfish.Transit;

public class AuthlogCriteriaDto
{
	public string UserId { get; set; }

	public string Username { get; set; }

	public string Source { get; set; }

	public string GrantType { get; set; }

	public DateTime? From { get; set; }

	public DateTime? To { get; set; }

	public bool? Success { get; set; }
}
