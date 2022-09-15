using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WasteController : MonoBehaviour
{
    [SerializeField]
    private Button newDayButton;
    public TextMeshProUGUI wasteText;
    public GarbagePile garbagePile;

    //Helper Array with the same order as the probabilities array
    private readonly IngredientType[] foodTypes = new IngredientType[] 
    {
        IngredientType.Vegetable,
        IngredientType.Egg, 
        IngredientType.Meat, 
        IngredientType.MeatlessMeat,
        IngredientType.Water,
        IngredientType.SideDish,
        IngredientType.Cheese, 
        IngredientType.Bread, 
        IngredientType.CerealProduct, 
        IngredientType.Flour, 
        IngredientType.Dairy,
        IngredientType.Nut, 
        IngredientType.Fruit, 
        IngredientType.Sauce, 
        IngredientType.Fat, 
        IngredientType.Dip, 
        IngredientType.Sweetener, 
        IngredientType.Herb,
        IngredientType.Leftover
    };

    //Accumulation of foodTypeWeights. Has to be the value of the last foodTypeWeightsElements
    private readonly int totalWeight = 205;

    // Weight probabilities for FoodTypes to spawn on trash mountain
    private readonly int[] foodTypeWeights =
    {
        40,     //Vegetable
        50,     //Egg
        60,     //Meat
        70,     //MeatlessMeat
        80,     //Water
        90,     //SideDish
        100,     //Cheese
        110,     //Bread
        120,     //CerealProduct
        130,     //Flour
        140,     //Dairy
        150,     //Nut
        160,     //Fruit
        170,     //Sauce
        180,     //Fat
        190,     //Dip
        195,      //Sweetener
        200,      //Herb
        205        //Leftover
    };

    // Start is called before the first frame update
    void Start()
    {
        garbagePile = gameObject.AddComponent<GarbagePile>();

        Button btn = newDayButton.GetComponent<Button>();
        btn.onClick.AddListener(NewDay);
        btn.onClick.AddListener(SpawnFood);

        Invoke(nameof(SpawnFood), 1);
    }

    // Update is called once per frame
    void Update()
    {
        //wasteText.text = "Waste: " + garbagePile.wasteCounter.ToString();
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }*/
    }

    // Adds food to the garbage pile lists
    public void SpawnFood()
    {
        Debug.Log("FOOD SPAWNED");
        for (int i = 1; i <= 100; i++)        //5 per day in the first week
        {
            IngredientType ingredientType = CalculateFoodType();
            Ingredient newIngredient = XmlParser.GetRandomIngredientByType(ingredientType);

            garbagePile.AddFresh(Instantiate(newIngredient));
        }

        //Recipes Debug starts here
        List <Ingredient> debugList = new List<Ingredient>();

        Ingredient ing1 = new Ingredient();
        ing1.ingredientName = "Apple";
        ing1.status = IngredientStatus.Mushy;
        ing1.satiation = 2;
        ing1.garbage = 2;
        debugList.Add(ing1);

        //Recipes Debug starts here
        Ingredient ing2 = new Ingredient();
        ing2.ingredientName = "Apple";
        ing2.status = IngredientStatus.Mushy;
        ing2.satiation = 2;
        ing2.garbage = 2;
        debugList.Add(ing2);

        //Recipes Debug starts here
        Ingredient ing3 = new Ingredient();
        ing3.ingredientName = "Egg";
        ing3.type = IngredientType.Egg;
        ing3.satiation = 2;
        ing3.garbage = 2;
        debugList.Add(ing3);

        //Recipes Debug starts here
        Ingredient ing4 = new Ingredient();
        ing4.ingredientName = "Hazelnut";
        ing4.satiation = 2;
        ing4.garbage = 2;
        debugList.Add(ing4);

        Recipe recipe = new Recipe();
        Meal debugMeal = recipe.ProcessRecipe(debugList, CraftingStation.Oven);
        Debug.Log(debugMeal.mealName);
        //Recipes Debug ends here
    }

    private IngredientType CalculateFoodType()
    {
        return foodTypes[ChooseFoodType()];
    }

    // Chooses random foodtype according to weights
    private int ChooseFoodType()
    {
        int randomNumber = Random.Range(0, totalWeight);

        for (int i = 0; i < foodTypeWeights.Length; i++)
        {
            if (randomNumber <= foodTypeWeights[i])
            {
                return i;
            }
        }
        return 0;
    }

    public void NewDay()
    {
        garbagePile.DecreaseDurabilities();
    }
}
