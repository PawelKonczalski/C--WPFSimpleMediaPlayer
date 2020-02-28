using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace wpfMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            clip.Play();
            if (timer != null)
                timer.Start();
        }
        private void Pause(object sender, RoutedEventArgs e)
        {
            clip.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            clip.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void clip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clip.ScrubbingEnabled = true;
            clip.Stop();
        }

        private void clip_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimerSlider.Maximum = clip.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            totalTimeOfWatchedVideo.Content = clip.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimerSlider.Value = clip.Position.TotalSeconds;
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            clip.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void TimerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            clip.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void TimerSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            clip.Play();
            timer.Start();
        }
    }
}
