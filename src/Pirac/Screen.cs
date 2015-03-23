using System;
using System.Windows;

namespace Pirac
{
	public class Screen : BindableObject, IViewAware
	{
		private WeakReference<FrameworkElement> view;

		public void AttachView(FrameworkElement view)
		{
			this.view = new WeakReference<FrameworkElement>(view);
		}

		public void TryClose(bool? dialogResult = null)
		{
			FrameworkElement v;
			if (view != null && view.TryGetTarget(out v))
			{
				GetViewCloseAction(this, v, dialogResult);
			}
			else
			{
				view = null;
			}
		}

		private static Action GetViewCloseAction(object viewModel, FrameworkElement view, bool? dialogResult)
		{
			var viewType = view.GetType();
			var closeMethod = viewType.GetMethod("Close");

			if (closeMethod != null)
				return () =>
				{
					var isClosed = false;
					if (dialogResult != null)
					{
						var resultProperty = viewType.GetProperty("DialogResult");
						if (resultProperty != null)
						{
							resultProperty.SetValue(view, dialogResult, null);
							isClosed = true;
						}
					}

					if (!isClosed)
					{
						closeMethod.Invoke(view, null);
					}
				};

			return () => PiracRunner.GetLogger<Screen>().Info("TryClose requires a view with a Close method.");
		}
	}
}