using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace SeeSharp.WPF
{
    public class MediaViewModel : INotifyPropertyChanged
    {
        private MediaElement _element;

        public MediaViewModel(MediaElement element)
        {
            this._element = element;

            this.PositionChanged += (s, e) => this.UpdatePositionInfo();
        }

        private TimeSpan _position;

        public TimeSpan Position
        {
            get { return this._position; }
            set
            {
                this._position = value;
                this.PositionChanged(this, EventArgs.Empty);
            }
        }

        private string _positionText;

        public string PositionText
        {
            get { return this._positionText; }
            set
            {
                this._positionText = value;
                this.RaisePropertyChanged("PositionText");
            }
        }

        private string _durationText;

        public string DurationText
        {
            get { return this._durationText; }
            set
            {
                this._durationText = value;
                this.RaisePropertyChanged("DurationText");
            }
        }

        public void UpdatePositionInfo()
        {
            this.PositionText = this.Position.ToString("mm\\:ss");
        }

        public void UpdateDurationInfo()
        {
            this.DurationText = this._element.NaturalDuration.TimeSpan.ToString("mm\\:ss");
        }

        public event EventHandler PositionChanged = delegate { };

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}