using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientDrop : MonoBehaviour, IDropHandler
{
    public GameObject slotPrefab;
    public Transform slotParent;

    CookingView cookingView;

    public int maxIngredients = 7;

    public List<GameObject> ingredients = new List<GameObject>();
    public List<Ingredient> _ingredients = new List<Ingredient>();

    Recipe recipe;

    private void Start()
    {
        cookingView = FindObjectOfType<CookingView>();
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag != null && ingredients.Count < maxIngredients)
        {
            if (ingredients.Count == 0)
            {
                recipe = new Recipe();
            }

            Debug.Log("Slot dragged on!");
            GameObject slotInstance = Instantiate(slotPrefab, slotParent);
            slotInstance.name = "Ingredient " + Mathf.RoundToInt(Random.Range(0, 100));
            
            IngredientPreviewSlot ingredientPreviewSlot = slotInstance.GetComponent<IngredientPreviewSlot>();

            ingredientPreviewSlot.ingredientDrop = this;

            ingredients.Add(slotInstance);

            Ingredient newIngredient = pointerEventData.pointerDrag.GetComponent<InventorySlot>().slottedIngredient;

            _ingredients.Add(newIngredient);

            List<IList> addableIngredients = recipe.GetAddableIngredientTypes(_ingredients, CraftingStation.Pot); //TODO: Tatsächlich verwendete Station übergeben

            ingredientPreviewSlot.slottedIngredient = newIngredient;
            cookingView.RemoveItem(ingredientPreviewSlot.slottedIngredient);

            Destroy(FindObjectOfType<CursorSlot>().gameObject);

            AddableIngredients(addableIngredients);
        }
    }

    public void UpdateAddableIngredients()
    {
        List<IList> addableIngredients = recipe.GetAddableIngredientTypes(_ingredients, CraftingStation.Pot);   //TODO: Tatsächlich verwendete Station übergeben
        AddableIngredients(addableIngredients);
    }

    void AddableIngredients(List<IList> addableIngredients)
    {
        List<IngredientType> filterSpecificTypes = (List<IngredientType>)addableIngredients[0];
        List<Ingredient> filterSpecificIngredients = (List<Ingredient>)addableIngredients[1];

        foreach (IngredientType type in filterSpecificTypes)
        {
            Debug.Log("Possible Types: " + type);
        }

        foreach (Ingredient ing in filterSpecificIngredients)
        {
            Debug.Log("Possible Ingredients: " + ing);
        }

        cookingView.FilterAddableIngredients(filterSpecificTypes, filterSpecificIngredients);
    }

    /*
    void Cook()
    {
        if (recipe.ProcessRecipe(_ingredients) == null)
        {
            Debug.Log("Funzt nicht");
            return;
        }

        Meal meal;
    }
    */

    public void RemoveIngredient(IngredientPreviewSlot slot)
    {
        cookingView.AddItem(slot.slottedIngredient);
        _ingredients.Remove(slot.slottedIngredient);

        ingredients.Remove(slot.gameObject);
        Destroy(slot.gameObject);

        if (_ingredients.Count != 0)
        {
            UpdateAddableIngredients();
        }
    }
}
