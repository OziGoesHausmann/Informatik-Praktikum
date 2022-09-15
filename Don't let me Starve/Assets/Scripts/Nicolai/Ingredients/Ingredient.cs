using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewIngredient", menuName = "Ingredient/IngredientSO")]
public class Ingredient : ScriptableObject
{
    public int id;
    public string ingredientName;

    public IngredientType type;
    public IngredientSpoilType spoilType;
    public Rarity rarity;
    public IngredientStatus status = IngredientStatus.Fresh;

    public Sprite icon;

    public int durability;
    public int garbage;

    public float satiation;

    public bool vegan = false;
    public bool spoiled = false;
    public bool sedimented = false;

    public bool preserved = false;
    private List<string> veganDairyNames;
    private List<string> veganFatNames;
    private List<string> veganSweetenerNames;
    private List<string> veganSauceNames;

    private void Awake()
    {
        SetVeganDairies();
        SetVeganFats();
        SetVeganSweeteners();
        SetVeganSauces();
        SetVegan();
    }

    private void SetVeganDairies()
    {
        veganDairyNames = new List<string>();
        veganDairyNames.Add("Oatmilk");
        veganDairyNames.Add("Coconut Milk");
        veganDairyNames.Add("Almond Milk");
        veganDairyNames.Add("Soy Milk");
    }

    private void SetVeganFats()
    {
        veganFatNames = new List<string>();
        veganFatNames.Add("Linseed Oil");
        veganFatNames.Add("Margerine");
        veganFatNames.Add("Olive Oil");
        veganFatNames.Add("Rapeseed Oil");
        veganFatNames.Add("Sunflower Oil");
        veganFatNames.Add("Walnut Oil");
    }

    private void SetVeganSweeteners()
    {
        veganSweetenerNames = new List<string>();
        veganSweetenerNames.Add("Agave Syrup");
        veganSweetenerNames.Add("Xylitol");
        veganSweetenerNames.Add("Sugar");
    }

    private void SetVeganSauces()
    {
        veganSauceNames = new List<string>();
        veganSauceNames.Add("Curry Sauce");
        veganSauceNames.Add("Tomato Sauce");
    }

    private void SetVegan()
    {
        if (
            type == IngredientType.SideDish
            || type == IngredientType.MeatlessMeat
            || type == IngredientType.Water
            || type == IngredientType.Vegetable
            || type == IngredientType.Herb
            || type == IngredientType.Flour
            || type == IngredientType.Nut
            || type == IngredientType.Bread
            || type == IngredientType.Fruit
            || type == IngredientType.Dip
            || type == IngredientType.CerealProduct
            )
            vegan = true;

        foreach(string name in veganDairyNames)
        {
            if(name.Equals(this.ingredientName))
            {
                vegan = true;
                return;
            }
        }

        foreach (string name in veganFatNames)
        {
            if (name.Equals(this.ingredientName))
            {
                vegan = true;
                return;
            }
        }

        foreach (string name in veganSweetenerNames)
        {
            if (name.Equals(this.ingredientName))
            {
                vegan = true;
                return;
            }
        }

        foreach (string name in veganSauceNames)
        {
            if (name.Equals(this.ingredientName))
            {
                vegan = true;
                return;
            }
        }
    }
}
