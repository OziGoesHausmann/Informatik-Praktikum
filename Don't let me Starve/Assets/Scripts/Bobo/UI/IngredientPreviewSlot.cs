using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPreviewSlot : MonoBehaviour
{
    public IngredientDrop ingredientDrop;
    public Ingredient slottedIngredient;

    public void Delete()
    {
        ingredientDrop.RemoveIngredient(this);
    }
}
