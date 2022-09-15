using System.Xml.Serialization;

public enum CraftingStation
{
    [XmlEnum("Pot")]
    Pot,        //Topf
    [XmlEnum("Pan")]
    Pan,        //Pfanne
    [XmlEnum("Oven")]
    Oven,       //Ofen
    [XmlEnum("Worktop")]
    Worktop,    //Arbeitsfläche
    [XmlEnum("DryBox")]
    DryBox,     //Dörrkiste
    [XmlEnum("Smokehouse")]
    Smokehouse  //Räucherkammer
}