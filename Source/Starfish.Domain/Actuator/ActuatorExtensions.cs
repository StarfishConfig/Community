using System.Diagnostics.CodeAnalysis;

namespace Nerosoft.Starfish.Domain;

internal static class ActuatorExtensions
{
	public static Task<TResult> ReturnAsync<TTarget, TResult>(this Task<TTarget> source, [NotNull] Func<TTarget, TResult> selector)
	{
		return source.ContinueWith(task =>
		{
			return selector(task.Result);
		});
	}

	public static Task ReturnAsync<TTarget, TResult>(this Task<TTarget> source, [NotNull] Func<TTarget, TResult> selector, [NotNull] Action<TResult> action)
	{
		return source.ContinueWith(task =>
		{
			var result = selector(task.Result);
			action(result);
		});
	}

	public static Task ReturnAsync<TTarget>(this Task<TTarget> source, [NotNull] Action<TTarget> action)
	{
		return source.ContinueWith(task =>
		{
			action(task.Result);
		});
	}
}
