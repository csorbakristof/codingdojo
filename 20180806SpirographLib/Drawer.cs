using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace _20180806SpirographLib
{
    public class Drawer
    {
        public void Draw(Mat img, IStroke stroke, bool connectPoints)
        {
            Draw(img, stroke.ToArray(), connectPoints);
        }

        public void Draw(Mat img, SPoint[] points, bool connectPoints)
        {
            if (!connectPoints)
                DrawPointByPoint(img, points);
            else
                DrawConnecting(img, points);
        }

        private void DrawConnecting(Mat img, SPoint[] points)
        {
            for(int i=0; i<points.Length-1; i++)
            {
                Vec3b color = points[i].Color;
                Cv2.Line(img,
                    new Point(points[i].X, points[i].Y),
                    new Point(points[i + 1].X, points[i + 1].Y),
                    new Scalar(color.Item0, color.Item1, color.Item2),
                    points[i].LineWidth);
            }
        }

        private void DrawPointByPoint(Mat image, SPoint[] points)
        {
            var indexer = image.GetGenericIndexer<Vec3b>();
            foreach (var p in points)
                indexer[p.Y, p.X] = p.Color;
        }
    }
}
