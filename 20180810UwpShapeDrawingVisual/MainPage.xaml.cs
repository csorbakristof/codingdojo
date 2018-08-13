using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _20180810UwpShapeAndPath
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private List<PolygonModel> polygons = new List<PolygonModel>();
        private readonly Brush blueBrush = new SolidColorBrush(Windows.UI.Colors.Aqua);

        private void AddShapes_Click(object sender, RoutedEventArgs e)
        {
            var p = new PolygonModel() { Fill = blueBrush };
            p.Points.Add(new Point(10, 10));
            p.Points.Add(new Point(50, 10));
            p.Points.Add(new Point(50, 50));
            p.Points.Add(new Point(10, 50));
            polygons.Add(p);

            this.viewShapeBased.SetModel(polygons);
            this.viewVisualBased.SetModel(polygons);
        }
    }
}
