[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Pirac.Tests")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5", FrameworkDisplayName=".NET Framework 4.5")]
namespace Pirac
{
    public abstract class AbstractCommand<T> : Pirac.Commands.BaseCommand<T>, Pirac.ICommand<T>, Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand
    {
        public AbstractCommand(System.Func<T, bool> canExecuteMethod = null) { }
        public abstract void Execute(T obj);
    }
    public abstract class Attachment<T> : Pirac.IAttachment
    {
        protected T viewModel;
        protected Attachment() { }
        protected abstract void OnAttach();
    }
    public abstract class AwaitableAbstractCommand<T> : Pirac.Commands.BaseCommand<T>, Pirac.ICommand<T>, Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand
    {
        protected AwaitableAbstractCommand() { }
        public abstract System.Threading.Tasks.Task ExecuteAsync(T obj);
    }
    public class BindableObject : Pirac.IObservableDataErrorInfo, Pirac.IObservablePropertyChanged, Pirac.IObservablePropertyChanging, System.ComponentModel.INotifyDataErrorInfo, System.ComponentModel.INotifyPropertyChanged, System.ComponentModel.INotifyPropertyChanging, System.IDisposable
    {
        public BindableObject() { }
        public System.IObservable<Pirac.PropertyChangedData> Changed { get; }
        public bool ChangeNotificationEnabled { get; }
        public System.IObservable<Pirac.PropertyChangingData> Changing { get; }
        public System.IObservable<Pirac.DataErrorChanged> ErrorsChanged { get; }
        public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged;
        public event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged;
        public event System.ComponentModel.PropertyChangingEventHandler System.ComponentModel.INotifyPropertyChanging.PropertyChanging;
        public virtual void Dispose() { }
        protected void OnPropertyChanged(string propertyName, object before, object after) { }
        protected void OnPropertyChanging(string propertyName, object before) { }
        protected void ResetDataError(string propertyName) { }
        protected void SetDataError(string propertyName, string error) { }
        public System.IDisposable SuppressNotifications() { }
    }
    public static class Command
    {
        public static System.Windows.Input.ICommand Create(System.Action executeMethod) { }
        public static System.Windows.Input.ICommand Create(System.Action executeMethod, System.Func<bool> canExecuteMethod) { }
        public static Pirac.ICommand<T> Create<T>(System.Action<T> executeMethod) { }
        public static Pirac.ICommand<T> Create<T>(System.Action<T> executeMethod, System.Func<T, bool> canExecuteMethod) { }
        public static Pirac.IAsyncCommand CreateAsync(System.Func<System.Threading.Tasks.Task> executeMethod) { }
        public static Pirac.IAsyncCommand CreateAsync(System.Func<System.Threading.Tasks.Task> executeMethod, System.Func<bool> canExecuteMethod) { }
        public static Pirac.IAsyncCommand<T> CreateAsync<T>(System.Func<T, System.Threading.Tasks.Task> executeMethod) { }
        public static Pirac.IAsyncCommand<T> CreateAsync<T>(System.Func<T, System.Threading.Tasks.Task> executeMethod, System.Func<T, bool> canExecuteMethod) { }
    }
    public class DataErrorChanged
    {
        public DataErrorChanged(string propertyName, string error) { }
        public string Error { get; }
        public string PropertyName { get; }
    }
    public class HasViewBase : Pirac.BindableObject, Pirac.IHaveView
    {
        public HasViewBase() { }
        protected virtual bool CanClose(System.Windows.FrameworkElement view) { }
        protected virtual void OnViewAttached(System.Windows.FrameworkElement view) { }
        protected virtual void OnViewLoaded(System.Windows.FrameworkElement view) { }
        public void TryClose(System.Nullable<bool> dialogResult = null) { }
    }
    public interface IActivatable
    {
        void Activate();
        bool CanClose();
        void Deactivate(bool close);
    }
    public interface IAsyncCommand : Pirac.IAsyncCommand<object>, Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand { }
    public interface IAsyncCommand<in T> : Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand
    {
        bool CanExecute(T obj);
        System.Threading.Tasks.Task ExecuteAsync(T obj);
    }
    public interface IAttachment
    {
        void AttachTo(object obj);
    }
    public interface ICommand<in T> : Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand
    {
        bool CanExecute(T obj);
        void Execute(T obj);
    }
    public interface IContainer
    {
        void Configure(System.Collections.Generic.IEnumerable<System.Type> views, System.Collections.Generic.IEnumerable<System.Type> viewModels, System.Collections.Generic.IEnumerable<System.Type> attachments, Pirac.IConventionManager conventionManager);
        T GetInstance<T>();
        object GetInstance(System.Type type);
    }
    public interface IConventionManager
    {
        System.Collections.Generic.IEnumerable<System.Type> FindAllAttachments();
        System.Collections.Generic.IEnumerable<System.Type> FindAllViewModels();
        System.Collections.Generic.IEnumerable<System.Type> FindAllViews();
        System.Collections.Generic.IEnumerable<System.Type> FindMatchingAttachments(object viewModel);
        System.Type FindView(object viewModel);
    }
    public interface IHaveView
    {
        void AttachView(System.Windows.FrameworkElement view);
    }
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warn(string message);
    }
    public interface IObservableCommand : System.IDisposable, System.Windows.Input.ICommand { }
    public interface IObservableDataErrorInfo
    {
        System.IObservable<Pirac.DataErrorChanged> ErrorsChanged { get; }
    }
    public interface IObservablePropertyChanged
    {
        System.IObservable<Pirac.PropertyChangedData> Changed { get; }
    }
    public interface IObservablePropertyChanging
    {
        System.IObservable<Pirac.PropertyChangingData> Changing { get; }
    }
    public interface IRaiseCanExecuteChanged
    {
        void RaiseCanExecuteChanged();
    }
    public interface IWindowManager
    {
        System.Nullable<bool> ShowDialog<TViewModel>();
        System.Nullable<bool> ShowDialog(object viewModel);
        void ShowWindow<TViewModel>();
        void ShowWindow(object viewModel);
    }
    public class PiracContext
    {
        public PiracContext() { }
        public Pirac.Conventions.IConvention AttachmentConvention { get; set; }
        public System.Reactive.Concurrency.IScheduler BackgroundScheduler { get; set; }
        public Pirac.IContainer Container { get; set; }
        public System.Func<string, Pirac.ILogger> Logger { get; set; }
        public System.Reactive.Concurrency.IScheduler MainScheduler { get; set; }
        public Pirac.Conventions.IConvention ViewConvention { get; set; }
        public Pirac.Conventions.IConvention ViewModelConvention { get; set; }
        public Pirac.IWindowManager WindowManager { get; set; }
    }
    public static class PiracRunner
    {
        public static Pirac.IWindowManager WindowManager { get; }
        public static Pirac.ILogger GetLogger(string name) { }
        public static Pirac.ILogger GetLogger<TType>() { }
        public static void SetContext(Pirac.PiracContext context) { }
        public static void Start<T>(Pirac.PiracContext context = null) { }
    }
    public class PropertyChangedData
    {
        public PropertyChangedData(string propertyName, object before, object after) { }
        public object After { get; }
        public object Before { get; }
        public string PropertyName { get; }
    }
    public class PropertyChangedData<TProperty>
    {
        public PropertyChangedData(string propertyName, TProperty before, TProperty after) { }
        public TProperty After { get; }
        public TProperty Before { get; }
        public string PropertyName { get; }
    }
    public class PropertyChangingData
    {
        public PropertyChangingData(string propertyName, object before) { }
        public object Before { get; }
        public string PropertyName { get; }
    }
    public class PropertyChangingData<TProperty>
    {
        public PropertyChangingData(string propertyName, TProperty before) { }
        public TProperty Before { get; }
        public string PropertyName { get; }
    }
    public static class PublicExtensions
    {
        public static System.IObservable<Pirac.PropertyChangedData<TProperty>> CastPropertyType<TProperty>(this System.IObservable<Pirac.PropertyChangedData> observable) { }
        public static System.IObservable<Pirac.PropertyChangingData<TProperty>> CastPropertyType<TProperty>(this System.IObservable<Pirac.PropertyChangingData> observable) { }
        public static System.IDisposable Execute<T>(this System.IObservable<T> observable, System.Windows.Input.ICommand command) { }
        public static System.IDisposable Execute<T>(this System.IObservable<T> observable, Pirac.ICommand<T> command) { }
        public static System.IDisposable ExecuteAsync<T>(this System.IObservable<T> observable, Pirac.IAsyncCommand<T> command) { }
        public static System.IObservable<TSource> ObserveOnPiracBackground<TSource>(this System.IObservable<TSource> source) { }
        public static System.IObservable<TSource> ObserveOnPiracMain<TSource>(this System.IObservable<TSource> source) { }
        public static void RaiseCanExecuteChanged(this System.Windows.Input.ICommand command) { }
        public static System.IObservable<TSource> SubscribeOnPiracBackground<TSource>(this System.IObservable<TSource> source) { }
        public static System.IObservable<TSource> SubscribeOnPiracMain<TSource>(this System.IObservable<TSource> source) { }
        public static System.Collections.ObjectModel.ObservableCollection<T> ToCollection<T>(this System.IObservable<T> source) { }
        public static Pirac.IObservableCommand ToCommand(this System.IObservable<bool> canExecuteObservable, System.Func<object, System.Threading.Tasks.Task> action) { }
        public static Pirac.IObservableCommand ToCommand(this System.IObservable<Pirac.PropertyChangedData<bool>> canExecuteObservable, System.Func<object, System.Threading.Tasks.Task> action) { }
        public static Pirac.IObservableCommand ToCommand(this System.IObservable<bool> canExecuteObservable, System.Action<object> action) { }
        public static Pirac.IObservableCommand ToCommand(this System.IObservable<Pirac.PropertyChangedData<bool>> canExecuteObservable, System.Action<object> action) { }
        public static System.IObservable<Pirac.PropertyChangedData> WhenPropertiesChanged(this Pirac.IObservablePropertyChanged changed, params string[] propertyNames) { }
        public static System.IObservable<Pirac.PropertyChangedData<TProperty>> WhenPropertiesChanged<TProperty>(this Pirac.IObservablePropertyChanged changed, params string[] propertyNames) { }
        public static System.IObservable<Pirac.PropertyChangingData> WhenPropertiesChanging(this Pirac.IObservablePropertyChanging changing, params string[] propertyNames) { }
        public static System.IObservable<Pirac.PropertyChangingData<TProperty>> WhenPropertiesChanging<TProperty>(this Pirac.IObservablePropertyChanging changing, params string[] propertyNames) { }
        public static System.IObservable<Pirac.PropertyChangedData> WhenPropertyChanged(this Pirac.IObservablePropertyChanged changed, string propertyName) { }
        public static System.IObservable<Pirac.PropertyChangedData<TProperty>> WhenPropertyChanged<TProperty>(this Pirac.IObservablePropertyChanged changed, string propertyName) { }
        public static System.IObservable<Pirac.PropertyChangingData> WhenPropertyChanging(this Pirac.IObservablePropertyChanging changing, string propertyName) { }
        public static System.IObservable<Pirac.PropertyChangingData<TProperty>> WhenPropertyChanging<TProperty>(this Pirac.IObservablePropertyChanging changing, string propertyName) { }
    }
    public class ViewModelBase : Pirac.HasViewBase, Pirac.IActivatable
    {
        public ViewModelBase() { }
        public System.Collections.Generic.IReadOnlyList<Pirac.IActivatable> Children { get; }
        public bool IsActive { get; }
        public bool IsInitialized { get; }
        public void Activate() { }
        protected virtual void ActivateChildren() { }
        protected void AddChildren(params Pirac.IActivatable[] viewModels) { }
        public void Deactivate(bool close) { }
        protected virtual void DeactivateChildren(bool close) { }
        protected virtual void OnActivate(bool wasInitialized) { }
        protected virtual void OnDeactivate(bool close) { }
        protected virtual void OnInitialize() { }
    }
    [System.Windows.TemplatePartAttribute(Name="PART_Presenter", Type=typeof(System.Windows.Controls.ContentPresenter))]
    public class ViewModelControl : System.Windows.Controls.ContentControl
    {
        public ViewModelControl() { }
        public override void OnApplyTemplate() { }
        protected override void OnContentChanged(object oldContent, object newContent) { }
    }
    public class ViewModelConverter : System.Windows.Markup.MarkupExtension, System.Windows.Data.IValueConverter
    {
        public ViewModelConverter() { }
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) { }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) { }
        public override object ProvideValue(System.IServiceProvider serviceProvider) { }
    }
    public class ViewNotFoundException : System.Exception
    {
        public ViewNotFoundException() { }
        public ViewNotFoundException(string message) { }
        public ViewNotFoundException(string message, System.Exception inner) { }
        protected ViewNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
namespace Pirac.Commands
{
    public abstract class BaseCommand<T> : Pirac.IRaiseCanExecuteChanged
    {
        public BaseCommand(System.Func<T, bool> canExecuteMethod = null) { }
        public event System.EventHandler CanExecuteChanged;
        public bool CanExecute(T parameter) { }
        public void RaiseCanExecuteChanged() { }
        protected System.IDisposable StartExecuting() { }
    }
}
namespace Pirac.Conventions
{
    public class ConventionBrokenException : System.Exception
    {
        public ConventionBrokenException() { }
        public ConventionBrokenException(string message) { }
        public ConventionBrokenException(string message, System.Exception inner) { }
        protected ConventionBrokenException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
    public interface IConvention
    {
        string BaseName(System.Type type);
        bool Filter(System.Type type);
        bool IsVariant(System.Type type, string basename);
        void Verify(System.Type type);
    }
}