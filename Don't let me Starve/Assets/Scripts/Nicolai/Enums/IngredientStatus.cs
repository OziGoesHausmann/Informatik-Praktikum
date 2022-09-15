using System.Xml.Serialization;

public enum IngredientStatus
{
    [XmlEnum("Fresh")]
    Fresh,
    [XmlEnum("Mushy")]
    Mushy,
    [XmlEnum("Sedimented")]
    Sedimented,
    [XmlEnum("Conserved")]
    Conserved
}
