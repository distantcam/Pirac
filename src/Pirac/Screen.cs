using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Pirac
{
    public class Screen : ViewAware, IScreen
    {
        static readonly ILogger Log = PiracRunner.GetLogger<Screen>();
        bool isActive;
        bool isInitialized;

        public IList<IScreen> Screens { get; } = new ObservableCollection<IScreen>();

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

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            var initialized = false;

            if (!IsInitialized)
            {
                IsInitialized = initialized = true;
                OnInitialize();
            }

            ActivateChildren();

            IsActive = true;
            Log.Info($"Activating {this}.");

            OnActivate(initialized);
        }

        public void Deactivate(bool close)
        {
            if (IsActive || (IsInitialized && close))
            {
                DeactivateChildren(close);

                IsActive = false;
                Log.Info($"Deactivating {this}.");

                OnDeactivate(close);

                if (close)
                {
                    TryClose();
                    Log.Info($"Closed {this}.");
                }
            }
        }

        protected void AddScreens(params IScreen[] screens)
        {
            foreach (var screen in screens)
            {
                Screens.Add(screen);
            }
        }

        protected virtual void ActivateChildren()
        {
            foreach (var screen in Screens)
            {
                screen.Activate();
            }
        }

        protected virtual void DeactivateChildren(bool close)
        {
            foreach (var screen in Screens)
            {
                screen.Deactivate(close);
            }
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnActivate(bool wasInitialized)
        {
        }

        protected virtual void OnDeactivate(bool close)
        {
        }

        bool IScreen.CanClose()
        {
            foreach (var screen in Screens)
            {
                if (!screen.CanClose())
                    return false;
            }

            return CanClose();
        }
    }
}