using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Pirac.Internal
{
    internal class WindowManager : IWindowManager
    {
        public bool? ShowDialog<TViewModel>() => ShowDialog(PiracRunner.Container.GetInstance<TViewModel>());

        public bool? ShowDialog(object viewModel) => CreateWindow(viewModel, true).ShowDialog();

        public void ShowWindow<TViewModel>() => ShowWindow(PiracRunner.Container.GetInstance<TViewModel>());

        public void ShowWindow(object viewModel)
        {
            NavigationWindow navWindow = null;

            if (Application.Current != null && Application.Current.MainWindow != null)
            {
                navWindow = Application.Current.MainWindow as NavigationWindow;
            }

            if (navWindow != null)
            {
                var window = CreatePage(viewModel);
                navWindow.Navigate(window);
            }
            else
            {
                CreateWindow(viewModel, false).Show();
            }
        }

        private static Window CreateWindow(object viewModel, bool isDialog)
        {
            var view = EnsureWindow(viewModel, PiracRunner.GetViewForViewModel(viewModel), isDialog);

            ViewModelBinder.Bind(view, viewModel);

            var screen = viewModel as IActivatable;
            if (screen != null)
            {
                new WindowConductor(screen, view);
            }

            return view;
        }

        private static Window EnsureWindow(object viewModel, object view, bool isDialog)
        {
            var window = view as Window;

            if (window == null)
            {
                window = new Window
                {
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                var owner = InferOwnerOf(window);
                if (owner != null)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                }
                else
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }
            else
            {
                var owner = InferOwnerOf(window);
                if (owner != null && isDialog)
                {
                    window.Owner = owner;
                }
            }

            return window;
        }

        private static Window InferOwnerOf(Window window)
        {
            if (Application.Current == null)
            {
                return null;
            }

            var active = Application.Current.Windows.OfType<Window>()
                .Where(x => x.IsActive)
                .FirstOrDefault();
            active = active ?? Application.Current.MainWindow;
            return active == window ? null : active;
        }

        private static Page CreatePage(object viewModel)
        {
            var view = EnsurePage(viewModel, PiracRunner.GetViewForViewModel(viewModel));

            view.DataContext = viewModel;

            return view;
        }

        private static Page EnsurePage(object model, object view)
        {
            var page = view as Page;

            if (page == null)
            {
                page = new Page { Content = view };
            }

            return page;
        }

        class WindowConductor
        {
            IActivatable screen;
            Window window;

            public WindowConductor(IActivatable screen, Window window)
            {
                this.screen = screen;
                this.window = window;

                screen.Activate();

                window.Closing += Closing;
                window.Closed += Closed;
            }

            void Closing(object sender, CancelEventArgs e)
            {
                e.Cancel = !screen.CanClose();
            }

            void Closed(object sender, EventArgs e)
            {
                var window = (Window)sender;
                window.Closing -= Closing;
                window.Closed -= Closed;

                screen.Deactivate(true);
            }
        }
    }
}