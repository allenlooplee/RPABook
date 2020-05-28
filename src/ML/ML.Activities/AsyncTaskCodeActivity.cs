using System;
using System.Activities;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace ML.Activities
{
	/// <summary>
	///     ML AsyncTaskCodeActivity with result <T>
	/// </summary>
	public abstract class AsyncTaskCodeActivity<T> : AsyncCodeActivity, IDisposable
	{
		protected CancellationTokenSource _cancellationTokenSource;
		protected bool _tokenDisposed;

		protected override void Cancel(AsyncCodeActivityContext context)
		{
			if (!_tokenDisposed)
			{
				_cancellationTokenSource?.Cancel();
				_cancellationTokenSource?.Dispose();

				_tokenDisposed = true;
			}

			base.Cancel(context);
		}

		protected override void CacheMetadata(CodeActivityMetadata metadata)
		{
			base.CacheMetadata(metadata);
		}

		protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
		{
			if (!_tokenDisposed) _cancellationTokenSource?.Dispose();

			_cancellationTokenSource = new CancellationTokenSource();
			_tokenDisposed = false;

			var property = context.DataContext.GetProperties()[MLScope.MLContainerPropertyTag];
			var servicesApplication = property?.GetValue(context.DataContext);

			var taskCompletionSource = new TaskCompletionSource<T>(state);
			var task = ExecuteAsync(context, _cancellationTokenSource.Token, (servicesApplication ?? new Application()) as Application);

			task.ContinueWith(t =>
			{
				if (t.IsFaulted)
					taskCompletionSource.TrySetException(t.Exception.InnerException);
				else if (t.IsCanceled || _cancellationTokenSource.IsCancellationRequested)
					taskCompletionSource.TrySetCanceled();
				else
					taskCompletionSource.TrySetResult(t.Result);

				callback?.Invoke(taskCompletionSource.Task);
			});

			return taskCompletionSource.Task;
		}

		protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
		{
			var task = (Task<T>) result;

			if (task.IsFaulted) ExceptionDispatchInfo.Capture(task.Exception.InnerException).Throw();
			if (task.IsCanceled) context.MarkCanceled();

			OutputResult(context, task.Result);

			if (!_tokenDisposed)
			{
				_cancellationTokenSource?.Dispose();

				_tokenDisposed = true;
			}
		}

		protected abstract Task<T> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken, Application client);

		protected abstract void OutputResult(AsyncCodeActivityContext context, T result);


		#region IDisposable Support

		private bool _disposedValue; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
					if (!_tokenDisposed)
					{
						_cancellationTokenSource.Dispose();

						_tokenDisposed = true;
					}

				_disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}

		#endregion
	}

	/// <summary>
	///     ML AsyncTaskCodeActivity without result
	/// </summary>
	public abstract class AsyncTaskCodeActivity : AsyncCodeActivity, IDisposable
	{
		private CancellationTokenSource _cancellationTokenSource;
		private bool _tokenDisposed;

		protected override void Cancel(AsyncCodeActivityContext context)
		{
			if (!_tokenDisposed)
			{
				_cancellationTokenSource?.Cancel();
				_cancellationTokenSource?.Dispose();

				_tokenDisposed = true;
			}

			base.Cancel(context);
		}

		protected override void CacheMetadata(CodeActivityMetadata metadata)
		{
			base.CacheMetadata(metadata);
		}

		protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
		{
			if (!_tokenDisposed) _cancellationTokenSource?.Dispose();

			_cancellationTokenSource = new CancellationTokenSource();
			_tokenDisposed = false;

			var property = context.DataContext.GetProperties()[MLScope.MLContainerPropertyTag];
			var servicesApplication = property?.GetValue(context.DataContext);

			var taskCompletionSource = new TaskCompletionSource<Action<AsyncCodeActivityContext>>(state);
			var task = ExecuteAsync(context, _cancellationTokenSource.Token, (servicesApplication ?? new Application()) as Application);

			task.ContinueWith(t =>
			{
				if (t.IsFaulted)
					taskCompletionSource.TrySetException(t.Exception.InnerException);
				else if (t.IsCanceled || _cancellationTokenSource.IsCancellationRequested)
					taskCompletionSource.TrySetCanceled();
				//else
				//	taskCompletionSource.TrySetResult(t.Result);

				callback?.Invoke(taskCompletionSource.Task);
			});

			return taskCompletionSource.Task;
		}

		protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
		{
			var task = (Task/*<Action<AsyncCodeActivityContext >>*/) result;

			if (task.IsFaulted) ExceptionDispatchInfo.Capture(task.Exception.InnerException).Throw();
			if (task.IsCanceled) context.MarkCanceled();

			//task.Result?.Invoke(context);

			if (!_tokenDisposed)
			{
				_cancellationTokenSource?.Dispose();

				_tokenDisposed = true;
			}
		}

		protected abstract Task/*<Action<AsyncCodeActivityContext>>*/ ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken, Application client);


		#region IDisposable Support

		private bool _disposedValue; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
					if (!_tokenDisposed)
					{
						_cancellationTokenSource.Dispose();

						_tokenDisposed = true;
					}

				_disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}