using System.Xml.Serialization;

public enum MealType
{
    [XmlEnum("Stew")]
    Stew,               //Eintopf
    [XmlEnum("PanDish")]
    PanDish,            //Pfannengericht
    [XmlEnum("Pizza")]
    Pizza,              //Pizza
    [XmlEnum("Sandwich")]
    Sandwich,           //Belegtes Brot
    [XmlEnum("Cereals")]
    Cereals,            //Müsli
    [XmlEnum("PastaRiceShice")]
    PastaRiceShice,     //Pasta / Reis mit Scheiß
    [XmlEnum("Schnitzel")]
    Schnitzel,          //Schnitzel mit Beilage
    [XmlEnum("Pastry")]
    Pastry,             //Gebäck
    [XmlEnum("Cake")]
    Cake,                // Kuchen
    [XmlEnum("Special")]
    Special
}