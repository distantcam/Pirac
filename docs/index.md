# Pirac Documentation
Pirac is a reactive based MVVM library for WPF.

## Quickstart

To get started with Pirac all that's needed is to add the following method to the `App` code. You will need a `MainViewModel` to start.

```
protected override void OnStartup(StartupEventArgs e)
{
    PiracRunner.Start<MainViewModel>();
}
```

You can also provide a context for Pirac, which allows you to configure extra things.

`PiracRunner.Start<MainViewModel>(new PiracContext());`

## Recommended NuGets

[PropertyChanging.Fody](https://github.com/fody/propertychanging) & [PropertyChanged.Fody](https://github.com/fody/propertychanged) auto-implements property change code.  
[DynamicData](https://github.com/RolandPheasant/DynamicData) provides a set of tools for working with collections in a reactive way.  
[Reactive.EventAggregator](https://github.com/shiftkey/Reactive.EventAggregator) an EventAggregator for ViewModel <-> ViewModel decoupled communication if needed.  
[WPFConverters](https://github.com/distantcam/WPFConverters) provides a useful set of converters for WPF.  
[MaterialDesignInXamlToolkit](https://github.com/ButchersBoy/MaterialDesignInXamlToolkit) takes Google design ideas and provides a consistently-named set of brushes and other resources for creating beautiful looking apps.

## Pirac Context

`PiracContext` allows you to set various things needed by Pirac.

- Logger is a function that returns an `ILogger` given a name of a class for the logger.
- Container which overrides the internal LightInject IoC container with your own. 
- WindowManager for creating windows and dialogs.
- MainScheduler & BackgroundScheduler are `IScheduler` objects for scheduling reactive work on the main UI thread, and a background worker thread.
- AttachmentConvention, ViewConvention, and ViewModelConvention are convention types from [Conventional](https://github.com/distantcam/conventional) for defining Views, ViewModels, and Attachments for Pirac to automatically find.

## ViewModels
`BindableObject` provides a basic implementation for ViewModel objects. It also implements the following interfaces:

- `IObservablePropertyChanging` & `IObservablePropertyChanged` which provide a `Changing` and `Changed` observable property. Pirac also has extension methods to filter these observables based on property name.
- `IObservableDataErrorInfo` which provides an `ErrorsChanged` observable with errors based on `INotifyDataErrorInfo`.

There are also helper methods.

- `SuppressNotifications` which stops all notifications. It returns a disposable, which resumes notifications when the object is disposed.
- `OnPropertyChanging` and `OnPropertyChanged` which trigger PropertyChanging and PropertyChanged events and observables.
- `SetDataError` which sets the error message for a property.
- `ResetDataError` which clears the error message for a property.

## Attachments

Attachments are a way of splitting up your ViewModel using composability into reusable parts.

## Commands

The `Command` class is a static class with methods for creating command objects. It can create standard and async commands with automatic code to disable/re-enable the command while it's running.

But this is a reactive library. Therefore we can also create a command from a boolean observable to enable/disable the command.

`canExecuteObservable.ToCommand(() => { /* Code to execute */ });`