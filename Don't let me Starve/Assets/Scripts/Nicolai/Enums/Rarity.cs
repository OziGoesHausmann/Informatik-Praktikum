using System.Xml.Serialization;

public enum Rarity
{
    [XmlEnum("Normal")]
    Normal,
    [XmlEnum("Rare")]
    Rare,
    [XmlEnum("VeryRare")]
    VeryRare
}