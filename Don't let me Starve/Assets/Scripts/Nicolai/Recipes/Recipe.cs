using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Recipe
{
    protected MealType type;

    public CraftingStation station;

    protected string mealName;

    private Dictionary<IngredientType, int> currentTypeCountDict;
    private Dictionary<string, int> specificIngsCountDict;

    public Recipe()
    {
        InitializeCountDictionairies();
    }

    // ingredient types and specific ingredient type count dictionairies have to be initialized here
    private void InitializeCountDictionairies()
    {
        currentTypeCountDict = new Dictionary<IngredientType, int>();
        currentTypeCountDict.Add(IngredientType.Bread, 0);
        currentTypeCountDict.Add(IngredientType.CerealProduct, 0);
        currentTypeCountDict.Add(IngredientType.Cheese, 0);
        currentTypeCountDict.Add(IngredientType.Dairy, 0);
        currentTypeCountDict.Add(IngredientType.Dip, 0);
        currentTypeCountDict.Add(IngredientType.Egg, 0);
        currentTypeCountDict.Add(IngredientType.Fat, 0);
        currentTypeCountDict.Add(IngredientType.Flour, 0);
        currentTypeCountDict.Add(IngredientType.Fruit, 0);
        currentTypeCountDict.Add(IngredientType.Herb, 0);
        currentTypeCountDict.Add(IngredientType.Leftover, 0);
        currentTypeCountDict.Add(IngredientType.Meat, 0);
        currentTypeCountDict.Add(IngredientType.MeatlessMeat, 0);
        currentTypeCountDict.Add(IngredientType.Nut, 0);
        currentTypeCountDict.Add(IngredientType.Sauce, 0);
        currentTypeCountDict.Add(IngredientType.Sweetener, 0);
        currentTypeCountDict.Add(IngredientType.Vegetable, 0);
        currentTypeCountDict.Add(IngredientType.Water, 0);
        currentTypeCountDict.Add(IngredientType.SideDish, 0);

        InitializeSpecificCountDict();

        Debug.Log("Check if dictionairy is correct.");
    }

    private void InitializeSpecificCountDict()
    {
        specificIngsCountDict = new Dictionary<string, int>();
        IEnumerable<XElement> recipeElements = XmlParser.GetAllRecipesFromXml();

        foreach (XElement recipe in recipeElements)
        {
            IEnumerable<XElement> nestedFixedOrAlternatives = recipe.Elements();
            foreach (XElement fixedOrAlternative in nestedFixedOrAlternatives)
            {
                IEnumerable<XElement> ingredients = fixedOrAlternative.Elements();
                foreach (XElement ingredient in ingredients)
                {
                    if (ingredient.Attribute("specific") != null)
                    {
                        if (!specificIngsCountDict.ContainsKey(ingredient.Attribute("specific").Value))
                            specificIngsCountDict.Add(ingredient.Attribute("specific").Value, 0);
                    }
                }
            }
        }
    }

    public List<IList> GetAddableIngredientTypes(List<Ingredient> ingredients, CraftingStation station)
    {
        List<IngredientType> ingTypes = new List<IngredientType>();
        List<Ingredient> specificIngs = new List<Ingredient>();

        foreach (Ingredient ing in ingredients)
        {
            currentTypeCountDict[ing.type] += 1;
            if (specificIngsCountDict.ContainsKey(ing.ingredientName))
                specificIngsCountDict[ing.ingredientName] += 1;
        }

        List<XElement> recipesContainingIngredients = GetRecipesWithIngredients(ingredients, station);

        foreach (XElement possibleRecipe in recipesContainingIngredients)
        {
            IEnumerable<XElement> recipeSection = possibleRecipe.Elements();

            foreach (XElement fixedOrAlternativeSection in recipeSection)
            {
                IEnumerable<XElement> ingsInSection = fixedOrAlternativeSection.Elements();

                if (fixedOrAlternativeSection.Name.ToString().Equals("Fixed"))
                {
                    foreach (XElement ing in ingsInSection)
                    {
                        if (
                            ing.Attribute("type") != null
                            && int.Parse(fixedOrAlternativeSection.Attribute("max").Value)
                            > currentTypeCountDict[Enum.Parse<IngredientType>(ing.Attribute("type").Value)]
                            )
                        {
                            ingTypes.Add(Enum.Parse<IngredientType>(ing.Attribute("type").Value));
                        }
                        else if (
                            ing.Attribute("specific") != null
                            && int.Parse(fixedOrAlternativeSection.Attribute("max").Value)
                            > specificIngsCountDict[ing.Attribute("specific").Value]
                            )
                        {
                            specificIngs.Add(XmlParser.StringToIngredient(ing.Attribute("specific").Value));
                        }
                    }
                }
                else if (fixedOrAlternativeSection.Name.ToString().Equals("Alternative"))
                {
                    //int totalAltIngCount = int.Parse(fixedOrAlternativeSection.Attribute("max").Value);
                    foreach(XElement ing in ingsInSection)
                    {
                        /*if (ing.Attribute("type") != null)
                            totalAltIngCount += currentTypeCountDict[Enum.Parse<IngredientType>(ing.Attribute("type").Value)];
                        else if (ing.Attribute("specific") != null)
                            totalAltIngCount += specificIngsCountDict[ing.Attribute("specific").Value];*/
                    }
                    
                    int totalAltIngCount = 0;
                    /*foreach (XElement ing in ingsInSection)
                    {
                        //foreach (XElement alternativeIng in fixedOrAlternativeSection.Elements("Ingredient"))
                        if (ing.Attribute("type") != null)
                        {
                            totalAltIngCount += currentTypeCountDict[Enum.Parse<IngredientType>(ing.Attribute("type").Value)];
                            if (int.Parse(fixedOrAlternativeSection.Attribute("max").Value) > totalAltIngCount)
                                ingTypes.Add(Enum.Parse<IngredientType>(ing.Attribute("type").Value));
                        }
                        else if (ing.Attribute("specific") != null)
                        {
                            if (int.Parse(fixedOrAlternativeSection.Attribute("max").Value) > totalAltIngCount)
                            {
                                Ingredient ingObj = XmlParser.StringToIngredient(ing.Attribute("specific").Value);
                                specificIngs.Add(ingObj);
                            }
                        }
                    }*/

                    foreach (XElement ing in ingsInSection)
                    {
                        if (int.Parse(fixedOrAlternativeSection.Attribute("max").Value) > totalAltIngCount)
                        {
                            // Hier scheint er entgegen der Abfrage zu agieren ._.
                            if (ing.Attribute("type") != null && !CheckIfIngredientTypeContainedInList(ingTypes, Enum.Parse<IngredientType>(ing.Attribute("type").Value)))
                                ingTypes.Add(Enum.Parse<IngredientType>(ing.Attribute("type").Value));
                            else if (ing.Attribute("specific") != null)
                            {
                                Ingredient specificIngredient = XmlParser.StringToIngredient(ing.Attribute("specific").Value);
                                if (!CheckIfIngredientContainedInList(specificIngs, specificIngredient))
                                    specificIngs.Add(specificIngredient);
                            }
                        }
                    }
                }
            }
        }

        List<IList> finalList = new List<IList>();
        finalList.Add(ingTypes);
        finalList.Add(specificIngs);
        return finalList;
    }

    private bool CheckIfIngredientTypeContainedInList(List<IngredientType> typeList, IngredientType ingType)
    {
        bool contained = false;
        foreach (IngredientType element in typeList)
        {
            if (element == ingType)
                contained = true;
        }

        return contained;
    }

    private bool CheckIfIngredientContainedInList(List<Ingredient> ingredientList, Ingredient ing)
    {
        bool contained = false;
        foreach (Ingredient element in ingredientList)
        {
            if (element.ingredientName.Equals(ing.ingredientName))
                contained = true;
        }
        return contained;
    }

    private List<XElement> GetRecipesWithIngredients(List<Ingredient> containedIngredients, CraftingStation station)
    {
        List<XElement> possibleRecipesElements = new List<XElement>();
        IEnumerable<XElement> recipes = XmlParser.GetAllRecipesFromXml();

        foreach (XElement el in recipes)
        {
            //debug
            if (el.Attribute("name").Value.Equals("Apfel-Biskuit"))
                Debug.Log(el);
            //debug end

            if (Enum.Parse<CraftingStation>(el.Attribute("station").Value) == station)
            {
                bool ingredientsContained = false;
                foreach (Ingredient ing in containedIngredients)
                {
                    if (el.ToString().Contains(ing.ingredientName) || el.ToString().Contains(ing.type.ToString()))
                        ingredientsContained = true;
                    else
                    {
                        ingredientsContained = false;
                        break;
                    }
                }
                if (ingredientsContained)
                    possibleRecipesElements.Add(el);
            }
        }
        return possibleRecipesElements;
    }

    public Meal ProcessRecipe(List<Ingredient> ingredients, CraftingStation station)
    {
        List<XElement> compatibleRecipes = GetRecipesWithIngredients(ingredients, station);
        List<Ingredient> ingredientsCopy = new List<Ingredient>(ingredients);               //All properly used ingredients are removed from this list during loop cycle 

        foreach (XElement recipe in compatibleRecipes)
        {
            foreach(XElement fixedOrAlternativeSection in recipe.Elements())
            {
                int minValue = int.Parse(fixedOrAlternativeSection.Attribute("min").Value);
                int maxValue = int.Parse(fixedOrAlternativeSection.Attribute("max").Value);
                int iterationValue = 0;
                bool sectionIngredientsComplete = false;

                IEnumerable<XElement> ingredientsInSection = fixedOrAlternativeSection.Elements();

                foreach(Ingredient usedIng in ingredients)
                {
                    if (fixedOrAlternativeSection.Name.ToString().Equals("Fixed")
                        || fixedOrAlternativeSection.Name.ToString().Equals("Alternative") && iterationValue < maxValue)
                    {
                        foreach (XElement recipeIng in ingredientsInSection)
                        {
                            if (
                                recipeIng.Attribute("type") != null
                                && Enum.Parse<IngredientType>(recipeIng.Attribute("type").Value) == usedIng.type
                                ||
                                recipeIng.Attribute("specific") != null
                                && recipeIng.Attribute("specific").Value.Equals(usedIng.ingredientName)
                                )
                            {
                                ingredientsCopy.Remove(usedIng);
                                iterationValue++;
                            }
                        }
                        if (iterationValue == minValue)
                            sectionIngredientsComplete = true;
                    }
                    else if(iterationValue == maxValue)
                        break;
                }
                if (!sectionIngredientsComplete)
                    return null;
            }
            if(ingredientsCopy.Count == 0)
                return CompoundRecipe(ingredients, recipe);
        }

        return null;    //no possible recipe fitted
    }

    private Meal CompoundRecipe(List<Ingredient> ingredients, XElement recipe)
    {
        Meal meal = new();

        meal.mealName = recipe.Attribute("name").Value;
        meal.type = Enum.Parse<MealType>(recipe.Attribute("type").Value);

        foreach (Ingredient ing in ingredients)
        {
            meal.garbage += ing.garbage;
            meal.satiation += ing.satiation;
        }

        return meal;
    }
}
