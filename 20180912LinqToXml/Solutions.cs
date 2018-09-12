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
            return Rects.Where( r => Texts.Any(t => IsInside(r, GetTextLocation(t))) );
        }

        internal IEnumerable<XElement> GetRectanglesWithStrokeWidth(int width)
        {
            return Rects.Where(r => r.Attribute("style").Value.Contains($"stroke-width:{width}"));
        }

        internal IEnumerable<string> GetColorOfRectanglesWithGivenX(double x)
        {
            return Rects
                .Where(r => double.Parse(r.Attribute("x").Value) == x)
                .Select(r=>GetColorOfRectangle(r));
        }

        private string GetColorOfRectangle(XElement rect)
        {
            string style = rect.Attribute("style").Value;
            int idx = style.IndexOf("fill:");
            return style.Substring(idx + 5, 7);
        }

        internal (string id1, string id2) GetSingleRectanglePairCloseToEachOther(double maxDistance)
        {
            foreach(var r1 in Rects)
                foreach(var r2 in Rects)
                    if (r1 != r2 && AreClose(r1,r2, maxDistance))
                        return (r1.Attribute("id").Value, r2.Attribute("id").Value);
            return (null, null);
        }

        private bool AreClose(XElement r1, XElement r2, double maxDistance)
        {
            var x1 = double.Parse(r1.Attribute("x").Value);
            var y1 = double.Parse(r1.Attribute("y").Value);
            var w1 = double.Parse(r1.Attribute("width").Value);
            var h1 = double.Parse(r1.Attribute("height").Value);
            var x2 = double.Parse(r2.Attribute("x").Value);
            var y2 = double.Parse(r2.Attribute("y").Value);
            var w2 = double.Parse(r2.Attribute("width").Value);
            var h2 = double.Parse(r2.Attribute("height").Value);
            if (x1 + w1 < x2 - maxDistance)
                return false;
            if (y1 + h1 < y2 - maxDistance)
                return false;
            if (x2 + w2 + maxDistance < x1)
                return false;
            if (y2 + h2 + maxDistance < y1)
                return false;
            return true;
        }

        internal string ConcatenateOrderedTextsInsideRectangles()
        {
            var txt = Texts.Where(t => Rects.Any(r => IsInside(r, GetTextLocation(t))))
                .Select(t => t.Value).OrderBy(s => s);
            var str = txt.Aggregate(new StringBuilder(), (sb, t) => sb.Append($", {t}"), sb => sb.ToString())
                .ToString();
            return str.Substring(2);
        }

        internal IEnumerable<string> GetTextsOutsideRectangles()
        {
            return Texts
                .Where(t => !Rects.Any(r => IsInside(r, GetTextLocation(t))))
                .Select(t => t.Value);
        }

        internal string GetSingleTextInSingleRectangleWithColor(string color)
        {
            var rect = GetRectanglesWithColor(color).First();
            foreach(var t in Texts)
            {
                var pos = GetTextLocation(t);
                if (IsInside(rect, pos))
                    return t.Value;
            }
            return null;
        }

        private IEnumerable<XElement> GetRectanglesWithColor(string color)
        {
            return Rects.Where(r => GetColorOfRectangle(r) == color);
        }

        private (double, double) GetTextLocation(XElement text)
        {
            var x = double.Parse(text.Attribute("x").Value);
            var y = double.Parse(text.Attribute("y").Value);
            return (x, y);
        }

        private bool IsInside(XElement rect, (double x, double y) p)
        {
            var x = double.Parse(rect.Attribute("x").Value);
            var y = double.Parse(rect.Attribute("y").Value);
            var w = double.Parse(rect.Attribute("width").Value);
            var h = double.Parse(rect.Attribute("height").Value);
            return (x <= p.x && p.x <= x + w && y <= p.y && p.y <= y + h);
        }

        internal (double x, double y) GetRectangleLocationById(string id)
        {
            return Rects
                .Where(r => r.Attribute("id").Value == id)
                .Select(r => (
                    double.Parse(r.Attribute("x").Value),
                    double.Parse(r.Attribute("y").Value)))
                .Single();
        }

        internal string GetIdOfRectangeWithLargestY()
        {
            var maxY = Rects.Select(r => double.Parse(r.Attribute("y").Value)).Max();
            return Rects
                .First(r => Math.Abs(double.Parse(r.Attribute("y").Value) - maxY) < 0.001)
                .Attribute("id").Value;
        }

        internal IEnumerable<string> GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            return Rects.Where(r => AtLeastTwiceAsHighAsWide(r)).Select(r=>r.Attribute("id").Value);
        }

        private bool AtLeastTwiceAsHighAsWide(XElement rect)
        {
            return double.Parse(rect.Attribute("height").Value)
                >= 2.0 * double.Parse(rect.Attribute("width").Value);
        }
        #endregion
    }
}
