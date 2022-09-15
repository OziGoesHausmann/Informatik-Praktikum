using System.Xml.Serialization;

public enum IngredientSpoilType
{
    [XmlEnum("Mushy")]
    Mushy,
    [XmlEnum("Sediment")]
    Sediment     //For ingredients that go instantly to the trash
}