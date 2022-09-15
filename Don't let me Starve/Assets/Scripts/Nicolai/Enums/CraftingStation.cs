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
    Worktop,    //Arbeitsfl�che
    [XmlEnum("DryBox")]
    DryBox,     //D�rrkiste
    [XmlEnum("Smokehouse")]
    Smokehouse  //R�ucherkammer
}