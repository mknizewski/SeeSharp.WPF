using SeeSharp.Infrastructure;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for FullScreenPage.xaml
    /// </summary>
    public partial class FullScreenPage : Window
    {
        private MediaViewModel _viewModel;
        private enum ButtonState { Play, Pause, Restart }
        private MediaElement _mediaElement;

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public FullScreenPage(MediaElement mediaElement)
        {
            this._mediaElement = mediaElement;
            InitializeComponent();
            InitializeMedia();
            InitalizeTimer();
        }

        private void InitializeMedia()
        {
            this.media.Source = _mediaElement.Source;
            this.DataContext = this._viewModel = new MediaViewModel(this.media);
            this.media.MediaOpened += Media_MediaOpened;
        }

        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
            this._viewModel.UpdateDurationInfo();

            this.media.Position = new TimeSpan(ViewFactory.CurrentPosition.Ticks);
            this.media.LoadedBehavior = this._mediaElement.LoadedBehavior;
            this.mediaVolume.Value = _mediaElement.Volume;
            this.media.Volume = _mediaElement.Volume;

            this._mediaElement.LoadedBehavior = MediaState.Pause;
        }

        private void InitalizeTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30.0);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.media.Source != null)
            {
                this.UpdatePlayBar();
                this.UpdatePosition();
                this.UpdatePlayPauseButton();
            }
        }

        private void UpdatePlayBar()
        {
            if (media.NaturalDuration.HasTimeSpan)
            {
                if (media.NaturalDuration.TimeSpan != TimeSpan.Zero)
                    playBar.Width = media.Position.TotalMilliseconds / media.NaturalDuration.TimeSpan.TotalMilliseconds * playCanvas.ActualWidth;
            }
        }

        private void playPauseButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonState = (ButtonState)this.playPauseButton.Tag;
            if (buttonState == ButtonState.Play)
            {
                media.LoadedBehavior = MediaState.Play;
            }
            else if (buttonState == ButtonState.Pause)
            {
                media.LoadedBehavior = MediaState.Pause;
            }
            else if (buttonState == ButtonState.Restart)
            {
                media.LoadedBehavior = MediaState.Stop;
                media.LoadedBehavior = MediaState.Play;
            }
        }

        private void UpdatePosition()
        {
            if (media.NaturalDuration.HasTimeSpan)
                positionText.Text = String.Format("{0}", media.Position.ToString(@"mm\:ss"));
        }

        private void UpdatePlayPauseButton()
        {
            this.playPauseButton.IsEnabled = true;
            this.playPauseButton.Tag = ButtonState.Play;
            MediaState mediaState = GetMediaState();

            if (media.Position == media.NaturalDuration && media.NaturalDuration.TimeSpan != TimeSpan.Zero)
            {
                this.playPauseButton.Tag = ButtonState.Restart;
                this.playPauseButton.Style = (Style)App.Current.Resources["ReplayButtonStyle"];
                this.scroll.ScrollToVerticalOffset(this.scroll.ScrollableHeight);
            }
            else if (mediaState == MediaState.Play)
            {
                this.playPauseButton.Tag = ButtonState.Pause;
                this.playPauseButton.Style = (Style)App.Current.Resources["PauseButtonStyle"];
            }
            else if (mediaState == MediaState.Pause || mediaState == MediaState.Stop)
            {
                this.playPauseButton.Tag = ButtonState.Play;
                this.playPauseButton.Style = (Style)App.Current.Resources["PlayButtonStyle"];
            }
            else
                this.playPauseButton.IsEnabled = false;
        }

        private MediaState GetMediaState()
        {
            FieldInfo hlp = typeof(MediaElement).GetField("_helper", BindingFlags.NonPublic | BindingFlags.Instance);
            object helperObject = hlp.GetValue(media);
            FieldInfo stateField = helperObject.GetType().GetField("_currentState", BindingFlags.NonPublic | BindingFlags.Instance);
            MediaState state = (MediaState)stateField.GetValue(helperObject);

            return state;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.media.Volume = this.mediaVolume.Value;
        }

        private void PlayCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(playCanvas);
            var relativePosition = position.X / playCanvas.ActualWidth;

            media.Position = new TimeSpan((long)(media.NaturalDuration.TimeSpan.Ticks * relativePosition));
        }

        private void fullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeScreen();
        }

        private void ChangeScreen()
        {
            this._mediaElement.Position = new TimeSpan(this.media.Position.Ticks);
            this._mediaElement.LoadedBehavior = this.media.LoadedBehavior;
            this._mediaElement.Volume = this.media.Volume;

            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);

            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                ChangeScreen();
            else if (e.Key == Key.Up)
                this.scroll.ScrollToVerticalOffset(0.0);
            else if (e.Key == Key.Down)
                this.scroll.ScrollToVerticalOffset(this.scroll.ScrollableHeight);
        }
    }
}
