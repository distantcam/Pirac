using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Pirac
{
    public class ViewModelBase : ObserveView, IObserveClose, IObserveActivation
    {
        static readonly ILogger Log = PiracRunner.GetLogger<ViewModelBase>();

        private ObservableCollection<IObserveActivation> children;

        private Subject<Unit> initialized;
        private Subject<bool> activated;
        private Subject<bool> deactivated;
        private Subject<Unit> closed;

        private IObservable<Unit> whenInitialized;
        private IObservable<bool> whenActivated;
        private IObservable<bool> whenDeactivated;
        private IObservable<Unit> whenClosed;

        private bool isActive;
        private bool isInitialized;

        private volatile int disposeSignaled;

        public ViewModelBase()
        {
            CanCloseCheck = () => true;

            initialized = new Subject<Unit>();
            whenInitialized = initialized.AsObservable();

            activated = new Subject<bool>();
            whenActivated = activated.AsObservable();

            deactivated = new Subject<bool>();
            whenDeactivated = deactivated.AsObservable();

            closed = new Subject<Unit>();
            whenClosed = closed.AsObservable();

            children = new ObservableCollection<IObserveActivation>();
        }

        public bool IsActive
        {
            get { return isActive; }
            private set
            {
                if (isActive == value)
                    return;

                var before = isActive;
                OnPropertyChanging(nameof(IsActive), before);
                isActive = value;
                var after = isActive;
                OnPropertyChanged(nameof(IsActive), before, after);
            }
        }

        public bool IsInitialized
        {
            get { return isInitialized; }
            private set
            {
                if (isInitialized == value)
                    return;

                var before = isInitialized;
                OnPropertyChanging(nameof(IsInitialized), before);
                isInitialized = value;
                var after = isInitialized;
                OnPropertyChanged(nameof(IsInitialized), before, after);
            }
        }

        Func<bool> canCloseCheck;

        public Func<bool> CanCloseCheck
        {
            get
            {
                return canCloseCheck;
            }
            set
            {
                canCloseCheck = value;
            }
        }

        public IObservable<Unit> WhenInitialized() => whenInitialized;

        public IObservable<bool> WhenActivated() => whenActivated;

        public IObservable<bool> WhenDeactivated() => whenDeactivated;

        public IObservable<Unit> WhenClosed() => whenClosed;

        public IReadOnlyList<IObserveActivation> Children => children;

        public void AddChildren(params IObserveActivation[] viewModels)
        {
            foreach (var screen in viewModels.Except(Children))
            {
                children.Add(screen);
            }
        }

        void IObserveActivation.Activate()
        {
            if (IsActive)
            {
                return;
            }

            var isInitialized = false;

            if (!IsInitialized)
            {
                IsInitialized = isInitialized = true;
                initialized.OnNext(Unit.Default);
            }

            ActivateChildren();

            IsActive = true;
            Log.Debug($"Activating {this}.");

            activated.OnNext(isInitialized);
        }

        void IObserveActivation.Deactivate(bool close)
        {
            if (IsActive || (IsInitialized && close))
            {
                DeactivateChildren(close);

                IsActive = false;
                Log.Debug($"Deactivating {this}.");

                deactivated.OnNext(close);

                if (close)
                {
                    closed.OnNext(Unit.Default);

                    Log.Debug($"Closed {this}.");
                }
            }
        }

        bool IObserveActivation.CanCloseAll()
        {
            return Children.All(c => c.CanCloseAll()) && CanCloseCheck();
        }

        protected virtual void ActivateChildren()
        {
            foreach (var screen in Children)
            {
                screen.Activate();
            }
        }

        protected virtual void DeactivateChildren(bool close)
        {
            foreach (var screen in Children)
            {
                screen.Deactivate(close);
            }
        }

        public override void Dispose()
        {
            if (Interlocked.Exchange(ref disposeSignaled, 1) != 0)
            {
                return;
            }
            if (initialized != null)
            {
                initialized.OnCompleted();
                initialized.Dispose();
                initialized = null;
            }
            if (activated != null)
            {
                activated.OnCompleted();
                activated.Dispose();
                activated = null;
            }
            if (deactivated != null)
            {
                deactivated.OnCompleted();
                deactivated.Dispose();
                deactivated = null;
            }

            base.Dispose();
        }
    }
}