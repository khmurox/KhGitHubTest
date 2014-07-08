using System.Windows;
using Microsoft.Win32;
using MuroxGitHubTest.Properties;

namespace MuroxGitHubTest.ViewModels
{
    public sealed class MainViewModel : MainViewModelBase
    {
        private double _left;
        private double _top;
        private bool _topmost;

        public MainViewModel()
        {
            Topmost = Settings.Default.Topmost;
            AdjustWindowPosition(Settings.Default.Left, Settings.Default.Top);

            // Move the window if necessary when a laptop is undocked, etc.
            SystemEvents.DisplaySettingsChanged += (sender, args) => AdjustWindowPosition(Left, Top);
        }

        public double Left
        {
            get { return _left; }
            set
            {
                if (_left != value)
                {
                    _left = value;
                    Settings.Default.Left = _left;

                    OnPropertyChanged("Left");
                }
            }
        }

        public double Top
        {
            get { return _top; }
            set
            {
                if (_top != value)
                {
                    _top = value;
                    Settings.Default.Top = _top;

                    OnPropertyChanged("Top");
                }
            }
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool Topmost
        {
            get { return _topmost; }
            set
            {
                if (_topmost != value)
                {
                    _topmost = value;
                    Settings.Default.Topmost = _topmost;

                    OnPropertyChanged("Topmost");
                }
            }
        }

        private void AdjustWindowPosition(double left, double top)
        {
            // Provide some extra room for the window when moved.
            double bumper = SystemParameters.WindowCaptionHeight;

            // Initialize and expand the window boundaries for a buffer amount.
            var window = new Rect(left, top, 320, 188);
            window.Inflate(bumper, bumper);

            // Initialze the virtual screen boundaries.
            var screen = new Rect(
                SystemParameters.VirtualScreenLeft,
                SystemParameters.VirtualScreenTop,
                SystemParameters.VirtualScreenWidth,
                SystemParameters.VirtualScreenHeight
                );

            // Adjust the window to current virtual screen area while maintaining the same relative position to center.
            // Add in the bumper size to offset the window as Windows does for default window corner positions.

            if (window.Left < screen.Left)
            {
                // Too far left of the current left side.
                left = screen.Left + bumper;
            }

            if (window.Top < screen.Top)
            {
                // Too high above the current top side.
                top = screen.Top + bumper;
            }

            if (window.Right > screen.Right)
            {
                // Too far right of the current right side.
                left = screen.Right - window.Width + bumper;
            }

            if (window.Bottom > screen.Bottom)
            {
                // Too low below the current bottom side.
                top = screen.Bottom - window.Height + bumper;
            }

            Left = left;
            Top = top;
        }
    }
}
