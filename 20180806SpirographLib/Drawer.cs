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
        public void Draw(Mat img, Stroke stroke)
        {
            var indexer = img.GetGenericIndexer<Vec3b>();
            foreach (var p in stroke)
                indexer[p.Y, p.X] = p.Color;
        }
    }
}
