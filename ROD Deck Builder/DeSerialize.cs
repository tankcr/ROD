using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ROD_Deck_Builder
{
    [Serializable()]
    public class DeSerialize
    {
        [System.Xml.Serialization.XmlElement("Rarity")]
        public string Rarity { get; set; }

        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("Realm")]
        public string Realm { get; set; }

        [System.Xml.Serialization.XmlElement("MaxAtk")]
        public string MaxAtk { get; set; }

        [System.Xml.Serialization.XmlElement("MaxDef")]
        public string MaxDef { get; set; }

        [System.Xml.Serialization.XmlElement("Total")]
        public string Total { get; set; }

        [System.Xml.Serialization.XmlElement("Cost")]
        public string Cost { get; set; }

        [System.Xml.Serialization.XmlElement("AttEff")]
        public string AttEff { get; set; }

        [System.Xml.Serialization.XmlElement("DefEff")]
        public string DefEff { get; set; }

        [System.Xml.Serialization.XmlElement("OverallEff")]
        public string OverallEff { get; set; }

        [System.Xml.Serialization.XmlElement("Skill")]
        public string Skill { get; set; }

        [System.Xml.Serialization.XmlElement("EventSkl1")]
        public string EventSkl1 { get; set; }

        [System.Xml.Serialization.XmlElement("EventSkl2")]
        public string EventSkl2 { get; set; }
    }
    
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("CardCollection")]
    public class CardCollection
    {
        [XmlArray("Cards")]
        [XmlArrayItem("Card", typeof(Card))]
        public Card[] Card { get; set; }
    }
}
