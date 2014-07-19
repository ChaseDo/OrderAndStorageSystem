using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace KN_Order_Storage.Models
{
    [Serializable]
    public class AreaOption
    {
        [XmlAttribute]
        public string language { get; set; }

        [XmlArray, XmlArrayItem()]
        public List<Area> Areas { get; set; }
    }

    [Serializable]
    public class Area
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlElement]
        public string Name { get; set; }
    }

    [Serializable]
    public class ClientSourceOption
    {
        [XmlAttribute]
        public string language { get; set; }

        [XmlArray]
        public List<Source> Sources { get; set; }
    }

    [Serializable]
    public class Source
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlElement]
        public string Name { get; set; }
    }
}