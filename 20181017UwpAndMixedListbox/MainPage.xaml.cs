using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _20181017UwpAndMixedListbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Stroke> StrokeList = new ObservableCollection<Stroke>();

        public MainPage()
        {
            this.InitializeComponent();
            StrokeList.Add(new Stroke() { Title = "Elso elem" });
            StrokeList.Add(new Stroke() { Title = "Masodik elem" });
            StrokeList.Add(new RotatingStroke() { Title = "Harmadik (rot) elem", RotationCount=5 });

        }
    }
}
