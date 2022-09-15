using System.Xml.Serialization;

public enum IngredientType
{
    [XmlEnum("SideDish")]
    SideDish,       //Beilage
    [XmlEnum("Egg")]
    Egg,            //Ei
    [XmlEnum("Meat")]
    Meat,           //Fleisch
    [XmlEnum("MeatlessMeat")]
    MeatlessMeat,   //Fleischersatz
    [XmlEnum("Water")]
    Water,          //Wasser
    [XmlEnum("Vegetable")]
    Vegetable,      //Gemüse
    [XmlEnum("Cheese")]
    Cheese,         //Käse
    [XmlEnum("Herb")]
    Herb,           //Kräuter
    [XmlEnum("Flour")]
    Flour,          //Mehl
    [XmlEnum("Dairy")]
    Dairy,          //Milchprodukt
    [XmlEnum("Nut")]
    Nut,            //Nüsse und Kerne
    [XmlEnum("Fruit")]
    Fruit,          //Obst
    [XmlEnum("Sauce")]
    Sauce,          //Sauce
    [XmlEnum("Fat")]
    Fat,            //Speisefett
    [XmlEnum("Sweetener")]
    Sweetener,      //Süßungsmittel
    [XmlEnum("Dip")]
    Dip,            //Aufstrich
    [XmlEnum("Bread")]
    Bread,          //Brot
    [XmlEnum("CerealProduct")]
    CerealProduct,  //Getreideprodukt
    [XmlEnum("Leftover")]
    Leftover        //Essensreste / Leftover
}