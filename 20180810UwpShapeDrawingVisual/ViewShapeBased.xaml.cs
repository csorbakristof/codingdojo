using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace _20180810UwpShapeAndPath
{
    public sealed partial class ViewShapeBased : UserControl
    {
        public ViewShapeBased()
        {
            this.InitializeComponent();
        }

        internal void SetModel(List<PolygonModel> polygons)
        {
            this.canvas.Children.Clear();
            foreach (var p in polygons)
                this.canvas.Children.Add(CreateViewForModel(p));
        }

        private UIElement CreateViewForModel(PolygonModel p)
        {
            var s = new Polygon();
            foreach (var point in p.Points)
                s.Points.Add(point);
            s.Fill = p.Fill;
            return s;
        }

    }
}
