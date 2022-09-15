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
    Cereals,            //M�sli
    [XmlEnum("PastaRiceShice")]
    PastaRiceShice,     //Pasta / Reis mit Schei�
    [XmlEnum("Schnitzel")]
    Schnitzel,          //Schnitzel mit Beilage
    [XmlEnum("Pastry")]
    Pastry,             //Geb�ck
    [XmlEnum("Cake")]
    Cake,                // Kuchen
    [XmlEnum("Special")]
    Special
}