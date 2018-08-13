using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace _20180810UwpShapeAndPath
{
    class PolygonModel
    {
        public List<Point> Points { get; set; } = new List<Point>();
        public Brush Fill { get; set; }
    }
}
