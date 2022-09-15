using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveManager : MonoBehaviour
{
    public int smokeValue, potValue, pickleValue, dryValue;
    Ingredient preservedIngredient;

    public void Preserve(string station, Ingredient ingredient)
    {
        preservedIngredient = ingredient;

        switch (station)
        {
            case "Smoke":
                Smoke();
                break;
            case "Pot":
                Pot();
                break;
            case "Pickle":
                Pickle();
                break;
            case "Dry":
                Dry();
                break;
        }

        preservedIngredient.preserved = true;

        preservedIngredient = null;

        FindObjectOfType<CookingView>().UpdateUI();
    }

    public void Smoke()
    {
        preservedIngredient.durability += smokeValue;
        preservedIngredient.ingredientName += " (Smoked)";
    }

    public void Pot()
    {
        preservedIngredient.durability += potValue;
        preservedIngredient.ingredientName += " (Boiled)";
    }

    public void Pickle()
    {
        preservedIngredient.durability += pickleValue;
        preservedIngredient.ingredientName += " (Pickled)";
    }

    public void Dry()
    {
        preservedIngredient.durability += dryValue;
        preservedIngredient.ingredientName += " (Dried)";
    }
}
