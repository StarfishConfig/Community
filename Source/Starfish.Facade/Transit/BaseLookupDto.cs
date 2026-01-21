namespace Nerosoft.Starfish.Facade.Transit;

public abstract class BaseLookupDto<TKey>
{
	public TKey Id { get; set; }

	public string Name { get; set; }
}