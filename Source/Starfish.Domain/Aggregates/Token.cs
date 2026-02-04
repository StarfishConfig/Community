using Duende.IdentityModel;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a token aggregate for identity management.
/// </summary>
internal sealed class Token : EditableObjectBase<Token, string>
{
	private static readonly string[] _types = ["access_token", "refresh_token"];

	public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(p => p.Type);
	public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(p => p.Key);
	public static readonly PropertyInfo<string> SubjectProperty = RegisterProperty<string>(p => p.Subject);
	public static readonly PropertyInfo<DateTime> IssuesProperty = RegisterProperty<DateTime>(p => p.Issues);
	public static readonly PropertyInfo<DateTime?> ExpiresProperty = RegisterProperty<DateTime?>(p => p.Expires);

	/// <summary>
	/// Gets or sets the token type (e.g., access_token, refresh_token).
	/// </summary>
	public string Type
	{
		get => GetProperty(TypeProperty);
		private set => SetProperty(TypeProperty, value);
	}

	/// <summary>
	/// Gets or sets the key that is SHA256 encrypted.
	/// </summary>
	public string Key
	{
		get => GetProperty(KeyProperty);
		private set => SetProperty(KeyProperty, value);
	}

	/// <summary>
	/// Gets or sets the subject associated with the token (e.g., user ID).
	/// </summary>
	public string Subject
	{
		get => GetProperty(SubjectProperty);
		private set => SetProperty(SubjectProperty, value);
	}

	/// <summary>
	/// Gets or sets the issue time of the token.
	/// </summary>
	public DateTime Issues
	{
		get => GetProperty(IssuesProperty);
		private set => SetProperty(IssuesProperty, value);
	}

	/// <summary>
	/// Gets or sets the expiration time of the token.
	/// </summary>
	public DateTime? Expires
	{
		get => GetProperty(ExpiresProperty);
		private set => SetProperty(ExpiresProperty, value);
	}

	protected override void AddRules()
	{
		Rules.AddRule<Token>(TypeProperty, _ => Task.FromResult(!string.IsNullOrWhiteSpace(Type) && Type.IsIn(_types)), "Token type must be either 'access_token' or 'refresh_token'.");
		Rules.AddRule<Token>(IssuesProperty, _ => Task.FromResult(DateTime.MinValue < Issues && Issues <= DateTime.UtcNow), "Token issue time cannot be in the future.");
	}

	[FactoryCreate]
	private async Task CreateAsync(TokenCreateCommand command, CancellationToken cancellationToken = default)
	{
		Type = command.Type;
		Key = command.Token.ToSha256();
		Subject = command.Subject;
		Issues = command.Issues;
		Expires = command.Expires;
		await Task.CompletedTask;
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetService<ITokenRepository>();
		var data = new TokenData { Type = Type, Key = Key, Subject = Subject, Issues = Issues, Expires = Expires };
		return repository.SaveAsync(data, cancellationToken);
	}

	[FactoryDelete]
	private async Task DeleteAsync(TokenDeleteCommand command, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetService<ITokenRepository>();
		var key = command.Token.ToSha256();
		await repository.DeleteAsync(command.Type, key, cancellationToken);
	}
}