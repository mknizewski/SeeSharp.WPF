﻿using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using SeeSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for ModulePage.xaml
    /// </summary>
    public partial class ModulePage : UserControl
    {
        private const double Width720p = 1280.0;
        private const double Height720p = 720;
        private const double Width480p = 854.0;
        private const double Height480p = 480.0;
        private const double OneModuleFinished = 3.0;
        private const int CourseFinished = 100;
        private const string NotepadExecutable = "notepad.exe";

        private ModuleManager _moduleManager;
        private TimeSpan _currentVideoSpan;
        private MediaViewModel _viewModel;
        private bool _isFullScreen;

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        private enum ButtonState { Play, Pause, Restart }

        public ModulePage(string tag)
        {
            this._moduleManager = ModuleManager.GetModuleManager(tag);
            this._isFullScreen = false;

            InitializeComponent();
            InitializeView();
            InitializeModule();
            BeginCourseIfNotStarted();
            UpdateUserCourseAndUI();
            SetAchivmentIfNessesary();

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

        private void UpdatePosition()
        {
            if (media.NaturalDuration.HasTimeSpan)
                positionText.Text = String.Format("{0}", media.Position.ToString(@"mm\:ss"));
        }

        private void AdjustMediaMaxResolution(Size size)
        {
            double actualViewWidth = size.Width;
            double actualViewHeight = size.Height;
            double mediaWidth = double.NaN;
            double mediaHeight = double.NaN;

            if (_isFullScreen)
            {
                this.media.MaxWidth = size.Width;
                this.media.MaxHeight = size.Height;
            }
            else
            {
                mediaHeight = actualViewHeight >= Height720p ? Height720p : Height480p;
                mediaWidth = actualViewWidth >= Width720p ? Width720p : Width480p;

                this.media.MaxHeight = mediaHeight;
                this.media.MaxWidth = mediaWidth;
            }
        }

        private void SetAchivmentIfNessesary()
        {
            Achivments achivments;
            string tagModule = _moduleManager.CurrentModule.ModuleTag;

            achivments = tagModule == "1.1" ? Achivments.TechnologyPionier :
                tagModule == "2.1.3" ? Achivments.MakeVsGreatAgain :
                tagModule == "2.3.1" ? Achivments.DeclareVarNotWar :
                tagModule == "2.8" ? Achivments.ObjectiveJanusz :
                tagModule == "3.5" ? Achivments.KingOfNET :
                tagModule == "3.1.1" ? Achivments.ItsAPower : Achivments.None;

            if (achivments != Achivments.None)
            {
                WindowPage page = (WindowPage)App.Current.MainWindow;
                MainPage root = page.MainPage;

                root.SetAchivmentAlert(achivments);
            }
        }

        private void InitializeModule()
        {
            this.ModuleTitle.Text = _moduleManager.CurrentModule.ModuleName;

            string pathToTextModule = string.Format(AppSettingsDictionary.TextDirectory, _moduleManager.CurrentModule.ModuleTag.Replace('.', '_'));
            ServerServiceClient serviceClient = ServerServiceClient.GetInstance();
            this.ModuleTextBox.Text = serviceClient.GetModuleText(pathToTextModule);

            this.PervModule.IsEnabled = !_moduleManager.First;
            this.NextModule.IsEnabled = !_moduleManager.Last;

            this.Scroll.ScrollToVerticalOffset(0.0);
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            root.SectionBlock.Text = string.Format(AppSettingsDictionary.SectionPrefixPattern, _moduleManager.CurrentModule.ModuleName);
        }

        private void BeginCourseIfNotStarted()
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage mainPage = page.MainPage;
            UserManager userManager = mainPage.UserManager;
            string lastUserTagModule = userManager.UserInfo.LastTutorial;

            if (_moduleManager.First && string.IsNullOrEmpty(lastUserTagModule))
            {
                userManager.UserInfo.LastTutorial = _moduleManager.CurrentModule.ModuleTag;

                Dictionary<string, string> userProfile = userManager.UserProfileToDictionary();
                ServerServiceClient serverService = ServerServiceClient.GetInstance();

                serverService.UpdateUserProfile(userProfile);
            }
        }

        private void InitializeView()
        {
            if (_moduleManager.CurrentModule.ModuleTag.Equals("1.1") || _moduleManager.CurrentModule.ModuleTag.Equals("1.2"))
            {
                this.ModuleGrid.Visibility = Visibility.Collapsed;
                this.ProgramDownloadLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.ModuleGrid.Visibility = Visibility.Visible;
                this.ProgramDownloadLink.Visibility = Visibility.Visible;

                string pathToMovie = string.Format(@AppSettingsDictionary.VideoDirectory, _moduleManager.CurrentModule.ModuleTag);
                string absoluteUri = string.Concat(AppDomain.CurrentDomain.BaseDirectory, pathToMovie);

                this.media.Source = new Uri(absoluteUri);
            }

            this.DataContext = this._viewModel = new MediaViewModel(this.media);

            this.media.MediaOpened += this.MediaOpened;
            this.media.MediaFailed += this.MediaFailed;
            this.media.LoadedBehavior = MediaState.Stop;

            this.UpdateStatusText();
            this.UpdatePlayPauseButton();
        }

        public void Dispose()
        {
            _moduleManager.Dispose();
            _moduleManager = null;
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            this._viewModel.UpdateDurationInfo();
            //this.RestorePervousVideoPosition();
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

        private void PositionChanged(object sender, EventArgs e)
        {
            this.UpdatePlayBar();
            this.UpdatePlayPauseButton();
        }

        private void PlayCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(playCanvas);
            var relativePosition = position.X / playCanvas.ActualWidth;

            media.Position = new TimeSpan((long)(media.NaturalDuration.TimeSpan.Ticks * relativePosition));
        }

        private void MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            errorText.Visibility = Visibility.Visible;
            errorText.Text = e.ErrorException.ToString();
        }

        private void MediaCurrentStateChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateStatusText();
            this.UpdatePlayPauseButton();
        }

        private void UpdatePlayBar()
        {
            if (media.NaturalDuration.HasTimeSpan)
            {
                if (media.NaturalDuration.TimeSpan != TimeSpan.Zero)
                    playBar.Width = media.Position.TotalMilliseconds / media.NaturalDuration.TimeSpan.TotalMilliseconds * playCanvas.ActualWidth;
            }
        }

        private void UpdateBufferBar()
        {
            Debug.WriteLine("offset: " + media.BufferingProgress);
            Debug.WriteLine("progress: " + media.DownloadProgress);

            Canvas.SetLeft(playCanvas, media.BufferingProgress * playCanvas.ActualWidth);
            bufferBar.Width = media.DownloadProgress * playCanvas.ActualWidth;
        }

        private void UpdateStatusText()
        {
            errorText.Visibility = Visibility.Collapsed;
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

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;
            root.SetView(ViewType.WelcomePage, NavigationDictionary.WelcomePageView);
        }

        private void PervModule_Click(object sender, RoutedEventArgs e)
        {
            _moduleManager.ChangeModule(ActionModule.Perv);
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            root.SetModule(_moduleManager.CurrentModule.ModuleTag);
        }

        private void NextModule_Click(object sender, RoutedEventArgs e)
        {
            _moduleManager.ChangeModule(ActionModule.Next);
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            root.SetModule(_moduleManager.CurrentModule.ModuleTag);
        }

        private void UpdateUserCourseAndUI()
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage mainPage = page.MainPage;
            UserManager userManager = mainPage.UserManager;
            int currentModuleIndex = _moduleManager.GetIndexByTag(_moduleManager.CurrentModule.ModuleTag);
            int userModuleIndex = _moduleManager.GetIndexByTag(userManager.UserInfo.LastTutorial);

            if (currentModuleIndex > userModuleIndex)
            {
                int currentPercentage = Convert.ToInt32(Math.Ceiling(userManager.UserInfo.Percentage + OneModuleFinished));

                currentPercentage = currentPercentage > CourseFinished ? CourseFinished : currentPercentage;
                mainPage.ProgressPercentageTextBlock.Text = string.Format(AppSettingsDictionary.ShowPercentage, currentPercentage);
                userManager.UserInfo.LastTutorial = _moduleManager.CurrentModule.ModuleTag;
                userManager.UserInfo.Percentage = currentPercentage;

                Dictionary<string, string> userProfile = userManager.UserProfileToDictionary();
                ServerServiceClient serverService = ServerServiceClient.GetInstance();

                serverService.UpdateUserProfile(userProfile);
            }
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustMediaMaxResolution(e.NewSize);
        }

        private void fullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeScreen();
            _isFullScreen = !_isFullScreen;
        }

        private void RestorePervousVideoPosition()
        {
            if (_currentVideoSpan.Ticks != 0)
            {
                this.media.Position = _currentVideoSpan;
                this.media.Play();
            }
        }

        public void ChangeScreen()
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            ViewFactory.CurrentPosition = this.media.Position;
            FullScreenPage fullScreen = new FullScreenPage(this.media);

            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);

            fullScreen.Show();
        }

        private void ProgramDownloadLink_Click(object sender, RoutedEventArgs e)
        {
            string pathToTemplateProgram = string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                string.Format(
                    AppSettingsDictionary.ProgramFilesDirectory,
                    _moduleManager.CurrentModule.ModuleTag
                    )
                );

            if (File.Exists(pathToTemplateProgram))
                Process.Start(NotepadExecutable, pathToTemplateProgram);
            else
            {
                WindowPage page = (WindowPage)App.Current.MainWindow;
                MainPage root = page.MainPage;

                root.SetAlert(PageDictionary.FileNotAvaliable);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.media.Volume = this.mediaVolume.Value;
        }
    }
}