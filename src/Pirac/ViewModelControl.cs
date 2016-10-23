using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Pirac.Internal;

namespace Pirac
{
    [TemplatePart(Name = "PART_Presenter", Type = typeof(ContentPresenter))]
    public class ViewModelControl : ContentControl
    {
        static ViewModelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewModelControl), new FrameworkPropertyMetadata(typeof(ViewModelControl)));
        }

        ContentPresenter presenter;
        FrameworkElement view;

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (view != null && newContent != null)
            {
                ViewModelBinder.Bind(view, newContent);
            }
        }

        public override void OnApplyTemplate()
        {
            presenter = (ContentPresenter)GetTemplateChild("PART_Presenter");

            presenter.Loaded += Presenter_Loaded;
        }

        private void Presenter_Loaded(object sender, RoutedEventArgs e)
        {
            presenter.Loaded -= Presenter_Loaded;

            view = (FrameworkElement)VisualTreeHelper.GetChild((ContentPresenter)sender, 0);

            if (Content != null)
            {
                ViewModelBinder.Bind(view, Content);
            }
        }
    }
}