using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Ingredient/MealSO")]
public class Meal : ScriptableObject
{
    public string mealId;
    public string mealName;

    public MealType type;
    public CraftingStation station;

    public Sprite icon;

    public int durability = 3;
    public int garbage = 0;

    public float satiation = 0;

    public bool satisfactionBoost = false;

    public bool vegan;
    public bool spoiled = false;
    public bool sedimented = false;

}
