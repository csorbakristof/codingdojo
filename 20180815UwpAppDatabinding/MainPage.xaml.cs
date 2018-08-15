using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _20180815UwpAppDatabinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.TextDataModel = "DefaultText";
            this.TextBox.DataContext = this;
        }

        private string _TextDataModel;
        public string TextDataModel {
            get
            {
                return _TextDataModel;
            }
            set
            {
                if (_TextDataModel != value)
                {
                    _TextDataModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextDataModel"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetBtn_Click(object sender, RoutedEventArgs e)
        {
            TextDataModel = "Button clicked";
        }
    }
}
