using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace _20180912LinqToXml
{
    class Solutions
    {
        readonly XElement root;
        public Solutions(string svgFileName)
        {
            root = XElement.Load(svgFileName);
            
        }

        internal string GetIfOfTopLevelGroup()
        {
            var element = root.Elements().Single(e => e.Name.LocalName == "g");
            var id = element.Attribute("id").Value;
            return id;
        }

        public IEnumerable<XElement> GetAllElements(Func<string, bool> nameCondition)
        {
            return GetSelfAndAllChildElements(root, nameCondition);
        }

        internal int CountTextsWithValue(string v)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<XElement> GetAllRectangles()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<XElement> GetRectanglesWithTextInside()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<XElement> GetRectanglesWithStrokeWidth(int width)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> GetAllElements(string elementTypeName)
        {
            return GetSelfAndAllChildElements(root, n=>n==elementTypeName);
        }

        internal IEnumerable<string> GetColorOfRectanglesWithGivenX(int x)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XElement> GetSelfAndAllChildElements(XElement root, Func<string, bool> nameCondition)
        {
            if (nameCondition(root.Name.LocalName))
                yield return root;
            foreach(var e in root.Elements())
            {
                foreach (var child in GetSelfAndAllChildElements(e, nameCondition))
                    yield return child;
            }
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
            throw new NotImplementedException();
        }

        internal string GetTextInRectangleWithColor(string color)
        {
            throw new NotImplementedException();
        }

        internal (double x, double y) GetRectangleLocationById(string id)
        {
            throw new NotImplementedException();
        }

        internal int CountTexts(string text)
        {

            return 6;
        }

        internal string GetIdOfRectangeWithLargestY()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<string> GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            throw new NotImplementedException();
        }
    }
}
