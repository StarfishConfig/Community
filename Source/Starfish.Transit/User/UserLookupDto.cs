namespace Nerosoft.Starfish.Transit;

public class UserLookupDto : BaseLookupDto<string>
{
	public string Username { get; set; }

	public string Nickname { get; set; }
}