using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<Ingredient> inventory = new List<Ingredient>();

    public List<Ingredient> inventoryCategorised = new List<Ingredient>();

    public TextMeshProUGUI inventoryText;

    void Start()
    {

    }
    private void Update()
    {
        inventoryText.text = ListToText(inventory);
    }

    public void CategorisedInventory(IngredientType selectedType)
    {
        inventoryCategorised.Clear();

        foreach (Ingredient ingredient in inventory)
        {
            if (ingredient.type == selectedType)
            {
                inventoryCategorised.Add(ingredient);
            }
        }
    }

    public void CategorisedInventory(string category)
    {
        inventoryCategorised.Clear();

        List<IngredientType> categories = new List<IngredientType>();

        switch (category)
        {
            case "Vegetables, Fruits":
                categories.AddRange(new IngredientType[] { IngredientType.Vegetable, IngredientType.Fruit });
                break;
            case "Meat, Dairy, Cheese, Eggs":
                categories.AddRange(new IngredientType[] { IngredientType.Meat, IngredientType.Dairy, IngredientType.Cheese, IngredientType.Egg });
                break;
            case "Side Dish, Meatless Meat":
                categories.AddRange(new IngredientType[] { IngredientType.SideDish, IngredientType.MeatlessMeat });
                break;
            case "Water, Fat, Sweetener":
                categories.AddRange(new IngredientType[] { IngredientType.Water, IngredientType.Fat, IngredientType.Sweetener });
                break;
            case "Flour, Bread, Cereal Product":
                categories.AddRange(new IngredientType[] { IngredientType.Flour, IngredientType.Bread, IngredientType.CerealProduct });
                break;
            case "Herbs, Nuts":
                categories.AddRange(new IngredientType[] { IngredientType.Herb, IngredientType.Nut });
                break;
            case "Sauces, Dips":
                categories.AddRange(new IngredientType[] { IngredientType.Sauce, IngredientType.Dip });
                break;
            case "Meals":
                //categories.Add(IngredientType.Vegetable);
                break;
        }

        foreach (Ingredient ingredient in inventory)
        {
            if (categories.Contains(ingredient.type))
            {
                inventoryCategorised.Add(ingredient);
            }
        }
    }

    private string ListToText(List<Ingredient> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember.ingredientName.ToString() + "\n";
        }
        return result;
    }
}