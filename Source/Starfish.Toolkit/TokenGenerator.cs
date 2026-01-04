namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides factory methods to create a <see cref="TokenGeneratorBuilder"/> prepopulated with common claims.
/// </summary>
public static class TokenGenerator
{
    /// <summary>
    /// Creates a new <see cref="TokenGeneratorBuilder"/> with the specified subject claim.
    /// </summary>
    /// <param name="subject">The subject identifier to add as the <c>sub</c> claim.</param>
    /// <returns>A configured <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public static TokenGeneratorBuilder Create(string subject)
    {
        return Create(subject, null, null);
    }

    /// <summary>
    /// Creates a new <see cref="TokenGeneratorBuilder"/> with the specified subject and name claims.
    /// </summary>
    /// <param name="subject">The subject identifier to add as the <c>sub</c> claim.</param>
    /// <param name="name">The name to add as the <c>name</c> claim.</param>
    /// <returns>A configured <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public static TokenGeneratorBuilder Create(string subject, string name)
    {
        return Create(subject, name, null);
    }

    /// <summary>
    /// Creates a new <see cref="TokenGeneratorBuilder"/> with the specified subject, name and audience.
    /// </summary>
    /// <param name="subject">The subject identifier to add as the <c>sub</c> claim.</param>
    /// <param name="name">The name to add as the <c>name</c> claim.</param>
    /// <param name="audience">The audience to set on the token.</param>
    /// <returns>A configured <see cref="TokenGeneratorBuilder"/> instance.</returns>
    public static TokenGeneratorBuilder Create(string subject, string name, string audience)
    {
        var builder = new TokenGeneratorBuilder();
        if (subject != null)
        {
            builder.AddClaimSubject(subject);
        }

        if (name != null)
        {
            builder.AddClaimName(name);
        }

        if (audience != null)
        {
            builder.WithAudience(audience);
        }

        return builder;
    }
}