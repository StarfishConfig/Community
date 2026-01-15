namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides helper methods to run asynchronous tasks synchronously.
/// </summary>
public static class AsyncHelper
{
	/// <summary>
	/// Runs an asynchronous function synchronously and returns its result.
	/// </summary>
	/// <typeparam name="TResult">The type of the result returned by the asynchronous function.</typeparam>
	/// <param name="func">The asynchronous function to execute.</param>
	/// <returns>The result of the asynchronous function.</returns>
	public static TResult RunSync<TResult>(Func<Task<TResult>> func)
	{
		var oldContext = SynchronizationContext.Current;
		var context = new ExclusiveSynchronizationContext();
		SynchronizationContext.SetSynchronizationContext(context);
		TResult result = default;

		context.Post(Callback, null);
		context.BeginMessageLoop();
		SynchronizationContext.SetSynchronizationContext(oldContext);
		return result;

		async void Callback(object _)
		{
			try
			{
				result = await func();
			}
			catch (Exception exception)
			{
				context.Exception = exception;
				throw;
			}
			finally
			{
				context.EndMessageLoop();
			}
		}
	}

	/// <summary>
	/// Runs an asynchronous action synchronously.
	/// </summary>
	/// <param name="func">The asynchronous action to execute.</param>
	public static void RunSync(Func<Task> func)
	{
		var oldContext = SynchronizationContext.Current;
		var context = new ExclusiveSynchronizationContext();
		SynchronizationContext.SetSynchronizationContext(context);

		context.Post(Callback, null);
		context.BeginMessageLoop();

		SynchronizationContext.SetSynchronizationContext(oldContext);
		return;

		async void Callback(object _)
		{
			try
			{
				await func();
			}
			catch (Exception exception)
			{
				context.Exception = exception;
				throw;
			}
			finally
			{
				context.EndMessageLoop();
			}
		}
	}

	/// <summary>
	/// A custom synchronization context that allows running asynchronous tasks synchronously.
	/// </summary>
	private class ExclusiveSynchronizationContext : SynchronizationContext
	{
		private bool _done;

		/// <summary>
		/// Gets or sets the exception that occurred during the execution of a task.
		/// </summary>
		public Exception Exception { get; set; }

		private readonly AutoResetEvent _workItemsWaiting = new(false);
		private readonly Queue<Tuple<SendOrPostCallback, object>> _items = new();

		/// <summary>
		/// Throws a <see cref="NotSupportedException"/> as sending to the same thread is not supported.
		/// </summary>
		/// <param name="d">The delegate to call.</param>
		/// <param name="state">The state object to pass to the delegate.</param>
		public override void Send(SendOrPostCallback d, object state)
		{
			throw new NotSupportedException("We cannot send to our same thread");
		}

		/// <summary>
		/// Posts a callback to the synchronization context.
		/// </summary>
		/// <param name="callback">The callback to post.</param>
		/// <param name="state">The state object to pass to the callback.</param>
		public override void Post(SendOrPostCallback callback, object state)
		{
			lock (_items)
			{
				_items.Enqueue(Tuple.Create(callback, state));
			}

			_workItemsWaiting.Set();
		}

		/// <summary>
		/// Signals the end of the message loop.
		/// </summary>
		public void EndMessageLoop()
		{
			Post(_ => _done = true, null);
		}

		/// <summary>
		/// Starts the message loop to process posted tasks.
		/// </summary>
		public void BeginMessageLoop()
		{
			if (_done)
			{
				return;
			}

			while (!_done)
			{
				Tuple<SendOrPostCallback, object> task = null;
				lock (_items)
				{
					if (_items.Count > 0)
					{
						task = _items.Dequeue();
					}
				}

				if (task != null)
				{
					task.Item1(task.Item2);
					if (Exception != null) // the method threw an exception
					{
						throw new AggregateException("AsyncHelper.Run method threw an exception.", Exception);
					}
				}
				else
				{
					_workItemsWaiting.WaitOne();
				}
			}
		}

		/// <summary>
		/// Creates a copy of the current synchronization context.
		/// </summary>
		/// <returns>A copy of the current synchronization context.</returns>
		public override SynchronizationContext CreateCopy()
		{
			return this;
		}
	}
}