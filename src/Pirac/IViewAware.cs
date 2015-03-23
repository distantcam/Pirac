using System.Windows;

namespace Pirac
{
	public interface IViewAware
	{
		void AttachView(FrameworkElement view);
	}
}