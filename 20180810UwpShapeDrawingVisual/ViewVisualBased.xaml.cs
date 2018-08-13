using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace _20180810UwpShapeAndPath
{
    public sealed partial class ViewVisualBased : UserControl
    {
        public ViewVisualBased()
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
            PathFigure figure = CreatePathFigureFromPointList(p.Points);
            Geometry geometry = CreateGeometryFromFigure(figure);
            Path path = CreatePathFromGeometry(geometry, p.Fill);
            return path;
        }

        private PathFigure CreatePathFigureFromPointList(List<Point> points)
        {
            var figure = new PathFigure();
            figure.StartPoint = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                var segment = new LineSegment();
                segment.Point = points[i];
                figure.Segments.Add(segment);
            }
            figure.IsFilled = true;
            figure.IsClosed = true;
            return figure;
        }

        private Path CreatePathFromGeometry(Geometry geometry, Brush fill)
        {
            var path = new Path();
            path.Fill = fill;
            path.Data = geometry;
            return path;
        }

        private Geometry CreateGeometryFromFigure(PathFigure figure)
        {
            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
