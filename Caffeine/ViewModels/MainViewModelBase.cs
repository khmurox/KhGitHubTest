using Caffeine.Helper;

namespace Caffeine.ViewModels
{
    public abstract class MainViewModelBase : BindableObject
    {
        private NativeMethods.ExecutionState _current = NativeMethods.ExecutionState.Continuous;

	    protected MainViewModelBase()
        {
            DisplayRequired = Settings.Default.DisplayRequired;
            SystemRequired = Settings.Default.SystemRequired;
        }

        public virtual bool IsEnabled
        {
            get
            {
                return true;
            }
            private set
            {
                Update();
            }
        }

        public bool SystemRequired
        {
            get
            {
                return GetState(NativeMethods.ExecutionState.SystemRequired);
            }
            set
            {
                SetState(NativeMethods.ExecutionState.SystemRequired, value);
                Settings.Default.SystemRequired = value;
                OnPropertyChanged("SystemRequired");
            }
        }

        public bool DisplayRequired
        {
            get
            {
                return GetState(NativeMethods.ExecutionState.DisplayRequired);
            }
            set
            {
                SetState(NativeMethods.ExecutionState.DisplayRequired, value);
                Settings.Default.DisplayRequired = value;
                OnPropertyChanged("DisplayRequired");
            }
        }

        protected virtual void Update()
        {
            OnPropertyChanged("IsEnabled");
            OnPropertyChanged("InstallVisibility");
        }

        private bool GetState(NativeMethods.ExecutionState state)
        {
            return (_current & state) != 0;
        }

        private void SetState(NativeMethods.ExecutionState state, bool enable)
        {
            if (enable)
            {
                _current |= state;
            }
            else
            {
                _current &= ~state;
            }

            try
            {
                if (IsEnabled)
                {
                    NativeMethods.SetThreadExecutionState(_current);
                }
            }
            catch (System.MethodAccessException)
            {
                // TODO: Bubble up to errors collection.
            }
        }
    }
}
