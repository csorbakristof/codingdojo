using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace _20180912LinqToXml
{
    class Solutions
    {
        readonly XElement root;
        readonly XNamespace ns = "http://www.w3.org/2000/svg";

        public Solutions(string svgFileName)
        {
            root = XElement.Load(svgFileName);
        }

        private IEnumerable<XElement> Rects => root.Descendants(ns + "rect");
        private IEnumerable<XElement> Texts => root.Descendants(ns + "text");

        #region Methods representing the solution of the laboratory
        internal int CountTextsWithValue(string v)
        {
            return root.Descendants(ns + "text").Count(e => e.Value == v);
        }

        internal IEnumerable<XElement> GetAllRectangles()
        {
            return Rects;
        }

        internal IEnumerable<XElement> GetRectanglesWithTextInside()
        {
            return Rects.Where( r => Texts.Any(t => IsInside(r, t.GetLocation())) );
        }

        internal IEnumerable<XElement> GetRectanglesWithStrokeWidth(int width)
        {
            return Rects.Where(r => r.Attribute("style").Value.Contains($"stroke-width:{width}"));
        }

        internal IEnumerable<string> GetColorOfRectanglesWithGivenX(double x)
        {
            return Rects
                .Where(r => r.GetX() == x)
                .Select(r=>r.GetFillColor());
        }

        internal (string id1, string id2) GetSingleRectanglePairCloseToEachOther(double maxDistance)
        {
            foreach(var r1 in Rects)
                foreach(var r2 in Rects)
                    if (r1 != r2 && AreClose(r1,r2, maxDistance))
                        return (r1.Attribute("id").Value, r2.Attribute("id").Value);
            return (null, null);
        }

        internal string ConcatenateOrderedTextsInsideRectangles()
        {
            var txt = Texts.Where(t => Rects.Any(r => IsInside(r, t.GetLocation())))
                .Select(t => t.Value).OrderBy(s => s);
            var str = txt.Aggregate(new StringBuilder(), (sb, t) => sb.Append($", {t}"), sb => sb.ToString())
                .ToString();
            return str.Substring(2);
        }

        internal IEnumerable<string> GetTextsOutsideRectangles()
        {
            return Texts
                .Where(t => !Rects.Any(r => IsInside(r, t.GetLocation())))
                .Select(t => t.Value);
        }

        internal string GetSingleTextInSingleRectangleWithColor(string color)
        {
            var rect = GetRectanglesWithColor(color).First();
            foreach(var t in Texts)
            {
                var pos = t.GetLocation();
                if (IsInside(rect, pos))
                    return t.Value;
            }
            return null;
        }

        internal (double x, double y) GetRectangleLocationById(string id)
        {
            return Rects
                .Where(r => r.Attribute("id").Value == id)
                .Select(r => r.GetLocation()).Single();
        }

        internal string GetIdOfRectangeWithLargestY()
        {
            var maxY = Rects.Select(r => r.GetY()).Max();
            return Rects
                .First(r => Math.Abs(r.GetY() - maxY) < 0.001)
                .Attribute("id").Value;
        }

        internal IEnumerable<string> GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            return Rects.Where(r => AtLeastTwiceAsHighAsWide(r)).Select(r=>r.Attribute("id").Value);
        }
        #endregion

        private bool AtLeastTwiceAsHighAsWide(XElement rect)
        {
            return rect.GetHeight() >= 2.0 * rect.GetWidth();
        }

        private IEnumerable<XElement> GetRectanglesWithColor(string color)
        {
            return Rects.Where(r => r.GetFillColor() == color);
        }

        private bool IsInside(XElement rect, (double x, double y) p)
        {
            (double left, double top, double right, double bottom) = GetRectBoundaries(rect);
            return (left <= p.x && p.x <= right && top <= p.y && p.y <= bottom);
        }

        private bool AreClose(XElement r1, XElement r2, double maxDistance)
        {
            (double left1, double top1, double right1, double bottom1) = GetRectBoundaries(r1);
            (double left2, double top2, double right2, double bottom2) = GetRectBoundaries(r2);
            return (!(
                right1 < left2 - maxDistance ||
                bottom1 < top2 - maxDistance ||
                right2 + maxDistance < left1 ||
                bottom2 + maxDistance < top1));
        }

        private (double left,double top,double right,double bottom) GetRectBoundaries(XElement r)
        {
            var x = r.GetX();
            var y = r.GetY();
            var w = r.GetWidth();
            var h = r.GetHeight();
            return (x, y, x + w - 1, y + h - 1);
        }
    }
}
