using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
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

        internal (string id1, string id2) GetRectanglePairsCloseToEachOther()
        {
            throw new NotImplementedException();
        }

        internal string ConcatenateOrderedTextsInsideRectangles()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<string> GetTextsOutsideRectangles()
        {
            foreach(var t in Texts)
            {
                bool isOutsideRect = true;
                foreach(var r in Rects)
                {
                    if (IsInside(r, GetTextLocation(t)))
                    {
                        isOutsideRect = false;
                        break;
                    }
                }
                if (isOutsideRect)
                    yield return t.Value;
            }
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
