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

        #region A laborfeladatok megoldásai
        // Minden téglalap (rect elem) felsorolása
        internal IEnumerable<XElement> GetAllRectangles()
        {
            return Rects;
        }

        // Hány olyan szöveg van, aminek ez a tartalma?
        internal int CountTextsWithValue(string v)
        {
            return Texts.Count(e => e.Value == v);
        }

        #region Téglalap szűrések
        // Minden olyan rect elem felsorolása, aminek a kerete adott vastagságú.
        //  A keretvastagság (más beállításokkal együtt) a "style" szöveges attribútumban
        //  szerepel, pl. "stroke-width:2".
        internal IEnumerable<XElement> GetRectanglesWithStrokeWidth(int width)
        {
            return Rects.Where(r => r.Attribute("style").Value.Contains($"stroke-width:{width}"));
        }

        // Adott x koordinátájú téglalapok színének visszaadása szövegesen (pl. piros esetén "#ff0000").
        internal IEnumerable<string> GetColorOfRectanglesWithGivenX(double x)
        {
            return Rects
                .Where(r => r.GetX() == x)
                .Select(r => r.GetFillColor());
        }

        // Az adott ID-jú téglalap pozíciójának (x,y) visszaadása.
        internal (double x, double y) GetRectangleLocationById(string id)
        {
            return Rects
                .Where(r => r.Attribute("id").Value == id)
                .Select(r => r.GetLocation()).Single();
        }

        // A legnagyobb y értékkel rendezkező téglalap ID-jának visszaadása.
        internal string GetIdOfRectangeWithLargestY()
        {
            var maxY = Rects.Select(r => r.GetY()).Max();
            return Rects
                .First(r => Math.Abs(r.GetY() - maxY) < 0.001)
                .Attribute("id").Value;
        }

        // Minden olyan téglalap ID-jának felsorolása, ami legalább kétszer olyan magas mint széles.
        internal IEnumerable<string> GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            return Rects.Where(r => IsAtLeastTwiceAsHighAsWide(r)).Select(r => r.Attribute("id").Value);
        }
        #endregion

        #region Group kezelés
        // Adott ID-jú group-ban lévő téglalapok színét sorolja fel.
        internal IEnumerable<string> GetColorsOfRectsInGroup(string id)
        {
            var group = root.Descendants(ns + "g").Single(e => e.Attribute("id").Value == id);
            return group.Descendants(ns + "rect").Select(r => r.GetFillColor());
        }
        #endregion

        #region Téglalapok és szövegek viszonya
        // Minden olyan rect elem felsorolása, amiben van bármilyen szöveg.
        //  (Olyan rect, aminek a területén van egy szövegnek a kezdőpontja (x,y).)
        internal IEnumerable<XElement> GetRectanglesWithTextInside()
        {
            return Rects.Where( r => Texts.Any(t => IsInside(r, t.GetLocation())) );
        }

        // Adott színű téglalapon belüli szöveg visszaadása.
        //  Feltételezhetjük, hogy csak egyetlen ilyen színű téglalap van és abban egyetlen
        //  szöveg szerepel.
        internal string GetSingleTextInSingleRectangleWithColor(string color)
        {
            var rect = GetRectanglesWithColor(color).First();
            foreach (var t in Texts)
            {
                var pos = t.GetLocation();
                if (IsInside(rect, pos))
                    return t.Value;
            }
            return null;
        }

        // Minden téglalapon kívüli szöveg felsorolása.
        internal IEnumerable<string> GetTextsOutsideRectangles()
        {
            return Texts
                .Where(t => !Rects.Any(r => IsInside(r, t.GetLocation())))
                .Select(t => t.Value);
        }
        #endregion

        #region Téglalapok egymáshoz képesti viszonya
        // Az egyetlen olyan téglalap pár visszaadása (id attribútumuk értékével), amik legfeljebb
        //  adott távolságra vannak egymástól.
        // (Itt nem gond, ha foreach-et használsz, de jobb, ha nem.)
        internal (string id1, string id2) GetSingleRectanglePairCloseToEachOther(double maxDistance)
        {
            foreach(var r1 in Rects)
                foreach(var r2 in Rects)
                    if (r1 != r2 && AreClose(r1,r2, maxDistance))
                        return (r1.Attribute("id").Value, r2.Attribute("id").Value);
            return (null, null);
        }
        #endregion

        #region ILookup és Aggregate használata
        // Egy ILookup visszaadása, mely minden szöveghez megadja az ilyen szöveget tartalmazó
        //  téglalapok színét. (Az ILookup-ban csak azok a szövegek szerepelnek kulcsként, amikhez van
        //  is téglalap.)
        internal ILookup<string, string> GetBoundingRectangleColorListForEveryText()
        {
            return Texts.Select(t => (Text: t, Rect: Rects.SingleOrDefault(r => IsInside(r, t.GetLocation()))))
                .Where(i=>i.Rect!=null).ToLookup(i => i.Text.Value, i=>i.Rect?.GetFillColor());
        }

        // Minden téglalapon belüli szöveg ABC sorrendben egymás mögé fűzése, ", "-zel elválasztva.
        //  (Az "OrderBy(s=>s)" rendezése most elegendő lesz.)
        // Használd az Aggregate Linq metódust egy StringBuilderrel az összegyűjtéshez!
        internal string ConcatenateOrderedTextsInsideRectangles()
        {
            var txt = Texts.Where(t => Rects.Any(r => IsInside(r, t.GetLocation())))
                .Select(t => t.Value).OrderBy(s => s);
            var str = txt.Aggregate(new StringBuilder(), (sb, t) => sb.Append($", {t}"), sb => sb.ToString())
                .ToString();
            return str.Substring(2);
        }

        // Az adott kontúrszélességű (stroke width) téglalapok által együttesen lefedett terület
        //  szélességét és magasságát adja meg
        internal (double w, double h) GetEffectiveWidthAndHeight(int strokeThickness)
        {
            var rects = GetRectanglesWithStrokeWidth(strokeThickness);  // emph reuse!
            Boundary boundary = rects.Aggregate(
                new Boundary(),
                (b, r) => { b.UpdateToCoverRect(r); return b; });
            return (w: boundary.Width, h: boundary.Height);

        }
        #endregion
        #endregion

        class Boundary
        {
            public double Left = double.MaxValue;
            public double Top = double.MaxValue;
            public double Right = double.MinValue;
            public double Bottom = double.MinValue;

            public double Width => Right - Left + 1;
            public double Height => Bottom - Top + 1;

            public void UpdateToCoverRect(XElement rect)
            {
                Left = Math.Min(Left, rect.GetX());
                Right = Math.Max(Right, rect.GetX() + rect.GetWidth() - 1);
                Top = Math.Min(Top, rect.GetY());
                Bottom = Math.Max(Bottom, rect.GetY() + rect.GetHeight() - 1);
            }
        }

        private bool IsAtLeastTwiceAsHighAsWide(XElement rect)
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
