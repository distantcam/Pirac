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
        public static System.Windows.Input.ICommand Create(System.Action executeMethod) { }
        public static System.Windows.Input.ICommand Create(System.Action executeMethod, System.Func<bool> canExecuteMethod) { }
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
    public interface IViewAware
    {
        void AttachView(System.Windows.FrameworkElement view);
    }
    public class static PiracRunner
    {
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
    public class Screen : Pirac.BindableObject, Pirac.IViewAware
    {
        public Screen() { }
        public void AttachView(System.Windows.FrameworkElement view) { }
        public void TryClose(System.Nullable<bool> dialogResult = null) { }
    }
    public class static WindowManager
    {
        public static System.Nullable<bool> ShowDialog(object viewModel) { }
        public static void ShowWindow(object viewModel) { }
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
namespace Pirac.Internal
{
    
    public class Logger : Pirac.ILogger
    {
        public Logger() { }
        public void Debug(string message) { }
        public void Error(string message) { }
        public void Info(string message) { }
        public void Warn(string message) { }
    }
}