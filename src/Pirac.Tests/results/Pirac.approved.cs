[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5", FrameworkDisplayName=".NET Framework 4.5")]

namespace Pirac
{
    
    public abstract class Attachment<T> : Pirac.IAttachment
    
    {
        protected T viewModel;
        protected Attachment() { }
        protected abstract void OnAttach();
    }
    public class BindableObject : System.ComponentModel.INotifyPropertyChanged, System.ComponentModel.INotifyPropertyChanging
    {
        public BindableObject() { }
        public System.IObservable<Pirac.ReactivePropertyChangedEventArgs> Changed { get; }
        public bool ChangeNotificationEnabled { get; }
        public System.IObservable<Pirac.ReactivePropertyChangingEventArgs> Changing { get; }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
        protected void OnPropertyChanged(string propertyName) { }
        protected void OnPropertyChanging(string propertyName) { }
        public System.IDisposable SuppressNotifications() { }
    }
    public class static Command
    {
        public static Pirac.ICommand Create(System.Action executeMethod) { }
        public static Pirac.ICommand Create(System.Action executeMethod, System.Func<bool> canExecuteMethod) { }
        public static Pirac.ICommand<T> Create<T>(System.Action<T> executeMethod) { }
        public static Pirac.ICommand<T> Create<T>(System.Action<T> executeMethod, System.Func<T, bool> canExecuteMethod) { }
        public static Pirac.IAsyncCommand Create(System.Func<System.Threading.Tasks.Task> executeMethod) { }
        public static Pirac.IAsyncCommand Create(System.Func<System.Threading.Tasks.Task> executeMethod, System.Func<bool> canExecuteMethod) { }
        public static Pirac.IAsyncCommand<T> Create<T>(System.Func<T, System.Threading.Tasks.Task> executeMethod) { }
        public static Pirac.IAsyncCommand<T> Create<T>(System.Func<T, System.Threading.Tasks.Task> executeMethod, System.Func<T, bool> canExecuteMethod) { }
    }
    public class static CommandExtensions
    {
        public static void RaiseCanExecuteChanged(this System.Windows.Input.ICommand command) { }
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
    public interface ICommand : Pirac.ICommand<object>, Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand { }
    public interface ICommand<in T> : Pirac.IRaiseCanExecuteChanged, System.Windows.Input.ICommand
    
    {
        bool CanExecute(T obj);
        void Execute(T obj);
    }
    public interface IContainer
    {
        object GetInstance(System.Type type);
        void Setup(System.Collections.Generic.IEnumerable<System.Type> typesToRegister, System.Collections.Generic.IEnumerable<System.Type> viewModelTypesToRegister);
    }
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Info(string message);
        void Warn(string message);
    }
    public interface IRaiseCanExecuteChanged
    {
        void RaiseCanExecuteChanged();
    }
    public class static PiracRunner
    {
        public static Pirac.ILogger GetLogger<T>() { }
        public static Pirac.ILogger GetLogger(System.Type type) { }
        public static Pirac.ILogger GetLogger(string name) { }
        public static void Start<T>() { }
        public class static Default
        {
            public static System.Type AttachmentConvention { get; set; }
            public static Pirac.IContainer IoC { get; set; }
            public static System.Func<string, Pirac.ILogger> Logger { get; set; }
            public static System.Type ViewConvention { get; set; }
            public static System.Type ViewModelConvention { get; set; }
        }
    }
    public class ReactivePropertyChangedEventArgs
    {
        public ReactivePropertyChangedEventArgs(string propertyName, object sender) { }
        public string PropertyName { get; }
        public object Sender { get; }
    }
    public class ReactivePropertyChangingEventArgs
    {
        public ReactivePropertyChangingEventArgs(string propertyName, object sender) { }
        public string PropertyName { get; }
        public object Sender { get; }
    }
    public class static WindowManager
    {
        public static System.Nullable<bool> ShowDialog(object viewModel) { }
        public static void ShowWindow(object viewModel) { }
    }
}
namespace Pirac.Conventions
{
    
    public class AttachmentConvention : Conventional.Conventions.Convention
    {
        public AttachmentConvention() { }
    }
    public class ViewConvention : Conventional.Conventions.Convention
    {
        public ViewConvention() { }
    }
    public class ViewModelConvention : Conventional.Conventions.Convention
    {
        public ViewModelConvention() { }
    }
}