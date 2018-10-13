using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media;

namespace _20181013MorseUi
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand UpdateCommand;

        public MainViewModel()
        {
            UpdateCommand = new UpdateMorseCodeCommand(this);
        }

        private DoubleCollection _MorseLineDashArray = new DoubleCollection() { 1 };
        public DoubleCollection MorseLineDashArray {
            get { return _MorseLineDashArray; }
            set { SetIfChanged(ref _MorseLineDashArray, value); }
        }

        private void SetIfChanged<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (!member.Equals(value))
            {
                member = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
