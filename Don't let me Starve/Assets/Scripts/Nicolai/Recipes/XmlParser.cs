using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using System;

public static class XmlParser
{
    //Meal Types
    private static string stew = "Stew";
    private static string panDish = "PanDish";
    private static string pizza = "Pizza";
    private static string sandwich = "Sandwich";
    private static string cereals = "Cereals";
    private static string pastaRiceShice = "PastaRiceShice";
    private static string schnitzel = "Schnitzel";
    private static string pastry = "Pastry";
    private static string cake = "Cake";

    //Ingredient Types
    private static string sideDish = "SideDish";
    private static string egg = "Egg";
    private static string meat = "Meat";
    private static string meatlessMeat = "MeatlessMeat";
    private static string water = "Water";
    private static string vegetable = "Vegetable";
    private static string cheese = "Cheese";
    private static string herb = "Herb";
    private static string flour = "Flour";
    private static string dairy = "Dairy";
    private static string nut = "Nut";
    private static string fruit = "Fruit";
    private static string sauce = "Sauce";
    private static string fat = "Fat";
    private static string sweetener = "Sweetener";
    private static string dip = "Dip";
    private static string bread = "Bread";
    private static string cerealProduct = "CerealProduct";
    private static string leftover = "Leftover";

    public static IngredientType StringToIngredientType(string ingType)
    {
        if (ingType.Equals(sideDish))
            return IngredientType.SideDish;
        if (ingType.Equals(egg))
            return IngredientType.Egg;
        if (ingType.Equals(meat))
            return IngredientType.Meat;
        if (ingType.Equals(meatlessMeat))
            return IngredientType.MeatlessMeat;
        if (ingType.Equals(water))
            return IngredientType.Water;
        if (ingType.Equals(vegetable))
            return IngredientType.Vegetable;
        if (ingType.Equals(cheese))
            return IngredientType.Cheese;
        if (ingType.Equals(herb))
            return IngredientType.Herb;
        if (ingType.Equals(flour))
            return IngredientType.Flour;
        if (ingType.Equals(dairy))
            return IngredientType.Dairy;
        if (ingType.Equals(nut))
            return IngredientType.Nut;
        if (ingType.Equals(fruit))
            return IngredientType.Fruit;
        if (ingType.Equals(sauce))
            return IngredientType.Sauce;
        if (ingType.Equals(fat))
            return IngredientType.Fat;
        if (ingType.Equals(sweetener))
            return IngredientType.Sweetener;
        if (ingType.Equals(dip))
            return IngredientType.Dip;
        if (ingType.Equals(bread))
            return IngredientType.Bread;
        if (ingType.Equals(cerealProduct))
            return IngredientType.CerealProduct;
        if (ingType.Equals(leftover))
            return IngredientType.Leftover;

        // only returned if something is wrong. Don't know a way to fix it because Enum can't be null
        return IngredientType.Vegetable;
    }

    public static Ingredient StringToIngredient(string specificIngName)
    {
        XElement xelement = XElement.Load($"{Application.dataPath}/Databases/ingredients.xml");
        var ingredient = xelement.Elements("ingredient").First(x => x.Element("name").Value.Equals(specificIngName));

        Ingredient newIng = new Ingredient();

        newIng.ingredientName = ingredient.Element("name").Value;
        newIng.id = int.Parse(ingredient.Element("id").Value);
        newIng.type = Enum.Parse<IngredientType>(ingredient.Element("category").Value);
        newIng.rarity = Enum.Parse<Rarity>(ingredient.Element("rarity").Value);
        newIng.spoilType = Enum.Parse<IngredientSpoilType>(ingredient.Element("spoilType").Value);
        newIng.icon = Resources.Load("Sprites/Ingredients/" + ingredient.Element("name").Value) as Sprite;
        newIng.durability = int.Parse(ingredient.Element("durability").Value);
        newIng.garbage = int.Parse(ingredient.Element("garbage").Value);
        newIng.satiation = float.Parse(ingredient.Element("satiation").Value);

        return newIng;
    }

    public static Ingredient GetRandomIngredientByType(IngredientType type)
    {
        List<Ingredient> ingredientsOfType = new List<Ingredient>();

        XElement xelement = XElement.Load($"{Application.dataPath}/Databases/ingredients.xml");
        IEnumerable<XElement> allIngredients = xelement.Elements();

        List<XElement> allIngredientsList = allIngredients.ToList<XElement>();

        foreach(XElement el in allIngredientsList)
        {
            if(Enum.Parse<IngredientType>(el.Element("category").Value) == type)
            {
                Ingredient foundIngredient = StringToIngredient(el.Element("name").Value);
                ingredientsOfType.Add(foundIngredient);
            }
        }

        int index = UnityEngine.Random.Range(0, ingredientsOfType.Count);
        return ingredientsOfType[index];        
    }

    public static MealType ToMealType()
    {
        return MealType.Cake;   //Just compile error prevention
    }

    public static Recipe ToRecipe()
    {
        return null;
    }

    public static IEnumerable<XElement> GetAllRecipesFromXml()
    {
        XElement xelement = XElement.Load($"{Application.dataPath}/Databases/recipes.xml");
        IEnumerable<XElement> recipes = xelement.Elements();
        return recipes;
    }
}
