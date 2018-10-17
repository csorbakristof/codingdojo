// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace _20181017UwpAndMixedListbox
{
    public class RotatingStroke : Stroke, INotifyPropertyChanged
    {
        private int rotationCount = 1;
        public int RotationCount {
            get => rotationCount;
            set
            {
                if (rotationCount != value)
                {
                    rotationCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RotationCount"));
                }
            }
        }
        public ICommand IncreaseCountCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public RotatingStroke()
        {
            IncreaseCountCommand = new IncreaseCount() { Model = this };
        }

        public class IncreaseCount : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public RotatingStroke Model { get; set; }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                Model.RotationCount++;
            }
        }

    }
}
