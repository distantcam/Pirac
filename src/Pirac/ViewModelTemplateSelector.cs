using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Pirac
{
    public class ViewModelTemplateSelector : MarkupExtension
    {
        private static DataTemplateSelector selector = new InternalViewModelTemplateSelector();

        public override object ProvideValue(IServiceProvider serviceProvider) => selector;

        private class InternalViewModelTemplateSelector : DataTemplateSelector
        {
            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                if (item != null)
                {
                    var template = CreateTemplateForViewModel(item);
                    if (template != null)
                    {
                        return template;
                    }
                }

                return base.SelectTemplate(item, container);
            }

            private static DataTemplate CreateTemplateForViewModel(object viewModel)
            {
                if (!PiracRunner.IsContextSet)
                {
                    return CreateMessageTemplate(viewModel.GetType(), "View for", viewModel.ToString());
                }

                var viewType = PiracRunner.ConventionManager.FindView(viewModel);
                if (viewType == null)
                {
                    return CreateMessageTemplate(viewModel.GetType(), "Could not find view for", viewModel.ToString());
                }

                return CreateTemplate(viewModel.GetType(), viewType);
            }

            private static DataTemplate CreateTemplate(Type viewModelType, Type viewType)
            {
                var xaml = $"<DataTemplate DataType=\"{{x:Type vm:{viewModelType.Name}}}\"><v:{viewType.Name} /></DataTemplate>";

                var context = new ParserContext();

                context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
                context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
                context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

                context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                context.XmlnsDictionary.Add("vm", "vm");
                context.XmlnsDictionary.Add("v", "v");

                var template = (DataTemplate)XamlReader.Parse(xaml, context);
                return template;
            }

            private static DataTemplate CreateMessageTemplate(Type viewModelType, params string[] messages)
            {
                var rowDefs = Enumerable.Repeat($"<RowDefinition Height=\"auto\" />", messages.Length);
                var items = messages.Select((m, i) => $"<TextBlock Grid.Row=\"{i + 1}\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" Text=\"{m}\" />");

                var xaml = $@"<DataTemplate DataType=""{{x:Type vm:{viewModelType.Name}}}"">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height=""*"" />
{string.Join(Environment.NewLine, rowDefs)}
            <RowDefinition Height=""*"" />
        </Grid.RowDefinitions>
{string.Join(Environment.NewLine, items)}
    </Grid>
</DataTemplate>";

                var context = new ParserContext();

                context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
                context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);

                context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                context.XmlnsDictionary.Add("vm", "vm");

                var template = (DataTemplate)XamlReader.Parse(xaml, context);
                return template;
            }
        }
    }
}