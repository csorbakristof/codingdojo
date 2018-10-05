using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180919LinqToXmlCreate
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void CreateSimpleXml()
        {
            string riolitLocation = GetBaseXml().Elements("Kőzet")
                .Where(i => i.Element("Name").Value == "Riolit")
                .Select(j => j.Element("Location").Value).First();
            Assert.AreEqual("Mecsek", riolitLocation);
        }


        [TestMethod]
        public void AddNode()
        {
            var xml = GetBaseXml();
            var andezit = GetItemWithName(xml, "Andezit");
            andezit.Add(new XElement("Location", "Cserhát"));
            Assert.AreEqual(1, xml.Descendants("Location").Count(l => l.Value == "Cserhát"));
        }


        [TestMethod]
        public void GetDictionaryOfLocations()
        {
            var xml = GetBaseXml();
            var dict = xml.Elements("Kőzet")
                .ToDictionary(k => k.Element("Name").Value, l => l.Element("Location").Value);
            Assert.AreEqual("Balaton-felvidék", dict["Bazalt"]);
        }

        [TestMethod]
        public void GetItemsAfterBazalt()
        {
            var bazalt = GetItemWithName(GetBaseXml(), "Bazalt");
            var items = bazalt.ElementsAfterSelf();
            Assert.IsTrue(items.Any(k => k.Element("Name").Value == "Riolit"));
            Assert.IsTrue(items.Any(k => k.Element("Name").Value == "Gránit"));
            Assert.IsFalse(items.Any(k => k.Element("Name").Value == "Andezit"));
        }


        [TestMethod]
        public void SerializeXml()
        {
            var xml = GetBaseXml();
            var str = xml.ToString();
            Assert.IsTrue(str.Contains(@"<Location>Velencei-hg.</Location>"));
            Assert.IsFalse(str.Contains("Aragonit"));
        }


        [TestMethod]
        public void ParseXmlFromString()
        {
            var xmlString =
                @"<group>
                    <item>Egy</item>
                    <item>Kettő</item>
                </group>";
            var xml = XElement.Parse(xmlString);
            Assert.IsTrue(xml.Descendants("item").Any(x => x.Value == "Egy"));
            Assert.IsFalse(xml.Descendants("item").Any(x => x.Value == "Három"));
        }


        [TestMethod]
        public void ObjectSerializationAndDeserialization()
        {
            List<Item> list = new List<Item>();
            for (int i = 0; i < 10; i++)
                list.Add(new Item() { IntValue = i, StringValue = "Hello", IgnoredIntValue = 8 });
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, list);
            var xml = writer.ToString();
            Assert.IsTrue(xml.Contains(@"<IntValue>3</IntValue>"));
            var newList = serializer.Deserialize(new StringReader(xml)) as List<Item>;
            Assert.IsTrue(newList.Any(i => i.IntValue == 3));
            Assert.IsTrue(newList.All(i => i.IgnoredIntValue == 0));
            var item4 = XElement.Parse(xml).Elements("Item")
                .Single(i => i.Element("IntValue").Value == "4");
            Assert.AreEqual("Hello", item4.Element("StringValue").Value);
            Assert.IsNull(item4.Element("IgnoredIntValue"));
        }

        public class Item
        {
            public int IntValue { get; set; }
            public string StringValue { get; set; }

            [XmlIgnore]
            public int IgnoredIntValue { get; set; }

        }

        private XElement GetItemWithName(XElement xml, string name)
        {
            return xml.Elements("Kőzet")
                .Where(i => i.Element("Name").Value == name).First();
        }

        private XElement GetBaseXml() => new XElement("List",
                new XElement("Kőzet",
                    new XElement("Name", "Andezit"),
                    new XElement("Location", "Börzsöny")),
                new XElement("Kőzet",
                    new XElement("Name", "Bazalt"),
                    new XElement("Location", "Balaton-felvidék")),
                new XElement("Kőzet",
                    new XElement("Name", "Riolit"),
                    new XElement("Location", "Mecsek")),
                new XElement("Kőzet",
                    new XElement("Name", "Gránit"),
                    new XElement("Location", "Velencei-hg.")));
    }
}
