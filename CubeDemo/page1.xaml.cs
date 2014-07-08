using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace CubeDemo
{
    public partial class Page1
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            // setup trackball for moving the model around
            _trackball = new Trackball(true);
            _trackball.Attach(this);
            _trackball.Slaves.Add(MyViewport3D);
            _trackball.Enabled = true;
        }

        #region Events
        private void OnImage1Animate(object sender, RoutedEventArgs e)
        {
            var s = (Storyboard)FindResource("RotateStoryboard");
            BeginStoryboard(s);
        }

        #endregion

        Trackball _trackball;
    }
}



