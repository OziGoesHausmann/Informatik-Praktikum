using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using TMPro;

public class MoldieInteraction : MonoBehaviour
{
    [Header("Skillvalues")]
    public int strength;
    public int knowledge;
    public int endurance;
    public int satisfaction;
    public int hunger;

    [Header("Checks")]
    public bool satisChanged;
    public bool nextDayStarted;
    public bool notHungry;
    public int basicEndurance;
    public int basicStrength;
    public int maxHunger;
    public int hungryCounter;
    public int basicHunger;
    public int rawFoodEaten;

    [Header("Bar UI")]
    public bool activateBar;
    public bool inventoryFull;
    public float lootTime;
    public float dropTime;
    public Image bar;
    public GameObject canvas;

    [Header("Assignments")]
    public GameObject inventoryPos;
    public GameObject garbageLoot;
    public Transform inventoryTarget;
    public List<Ingredient> moldieInventory;


    [Header("Stat-UI")]
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI statsText;
    public bool showStats;
    public GameObject canvas2;

    //TEST
    public Ingredient eatingIngredient;


    private void Start()
    {
        basicStrength = strength;
        basicEndurance = endurance;
        hunger = basicHunger;
        
    }

    
    private void Update()
    {
        if (nextDayStarted)
        {
            SatisfationLevel();
            HungerMechanic();
            rawFoodEaten = 0;
            hunger = basicHunger;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showStats = false;
        }

            if (bar.fillAmount >= 1)
        {
            activateBar = false;

        }

        if (activateBar == true)
        {
            bar.fillAmount += 1 / lootTime * Time.deltaTime;
            gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            canvas.SetActive(true);
        }
        else
        {
            bar.fillAmount = 0;
            canvas.SetActive(false);

        }

        transform.GetChild(0).gameObject.SetActive(false);

        if(satisfaction < 0)
        {
            satisfaction = 0;
        }

        if(showStats == true)
        {
            ShowStats();
        }
        else
        {
            canvas2.SetActive(false);
        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trash" && !inventoryFull && endurance >= 3f)
        {
            Invoke("LootAction", lootTime);
            gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            activateBar = true;

        }


        if (other.tag == "Inventory" && inventoryFull)
        {
            inventoryFull = false;
            gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            Invoke("DropToInventory", dropTime);
            activateBar = true;
        }


    }

    void HungerMechanic()
    {
        //if (nextDayStarted && maxHunger > hunger)
        //{
        //    int malus = basicHunger - hunger;
        //    basicEndurance = endurance - malus;
        //    basicHunger = hunger;
        //}


        if(nextDayStarted && hunger != maxHunger)
        {
            hungryCounter += 1;
            nextDayStarted = false;
            notHungry = false;
            if (hungryCounter >= 2)
            {
                satisfaction -= 1;
            }         
            
        }

        if (hunger == maxHunger && !notHungry)
        {
            notHungry = true;
            if (notHungry)
            {
                hungryCounter = 0;
            }
        }
        if (hunger != maxHunger && notHungry)
        {
            notHungry = false;
            if (!notHungry)
            {
                hungryCounter = 0;
            }
        }

        

        if (nextDayStarted && hunger == maxHunger)
        {
            
            hungryCounter += 1;
            nextDayStarted = false;
            if (hungryCounter >= 2)
            {
                satisfaction += 1;
            }
        }

        

    }

    void SatisfationLevel()
    {
        if(satisfaction > 6)
        {
            satisfaction = 6;
        }

        if (satisfaction > 4)
        {
            endurance = basicEndurance;
            strength = basicStrength;
        }
        if (satisfaction == 4)
        {
            endurance = basicEndurance - 1;
        }
        if (satisfaction == 3)
        {
            endurance = basicEndurance - 1; ;
            strength = basicStrength - 1;
        }
        if (satisfaction == 2)
        {
            endurance = basicEndurance - 2;
            strength =  basicStrength - 2;
        }
        if (satisfaction <= 1)
        {
            endurance = 0;
        }
        
    }

    void LootAction()
    {
        gameObject.GetComponent<AIDestinationSetter>().pos = garbageLoot;
        gameObject.GetComponent<AIDestinationSetter>().target = inventoryTarget;
        gameObject.GetComponent<AIDestinationSetter>().enabled = true;
        TakeFromList();
        //NewItemInInvUI();

        endurance -= 3;

    }

    void TakeFromList()
    {
        inventoryFull = true;

        if (knowledge == 1)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 75)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                        
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 75 && rng < 95)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                        
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 95)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                        
                    }
                }


            }
        }

        if (knowledge == 2)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 65)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 65 && rng < 90)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 90)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }

        if (knowledge == 3)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 55)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 55 && rng < 85)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 85)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }

        if (knowledge == 4)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 45)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 45 && rng < 80)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 80)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }

        if (knowledge == 5)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 35)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 35 && rng < 75)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 75)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }

        if (knowledge == 6)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 25)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 25 && rng < 70)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 70)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }

        if (knowledge == 7)
        {
            for (int i = 0; i < strength; i++)
            {
                int rng = Random.Range(1, 101);
                Debug.Log(rng);

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().normal.Count; y++)
                {
                    if (moldieInventory[i] == null && rng <= 15)
                    {
                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().normal[y];
                        garbageLoot.GetComponent<GarbagePile>().normal[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng > 15 && rng < 65)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().rare[y];
                        garbageLoot.GetComponent<GarbagePile>().rare[y] = null;
                    }
                }

                for (int y = 0; y < garbageLoot.GetComponent<GarbagePile>().rare.Count; y++)
                {
                    if (moldieInventory[i] == null && rng >= 65)
                    {

                        moldieInventory[i] = garbageLoot.GetComponent<GarbagePile>().veryRare[y];
                        garbageLoot.GetComponent<GarbagePile>().veryRare[y] = null;
                    }
                }


            }
        }


    }

    void DropToInventory()
    {
        itemName.GetComponent<TextMeshProUGUI>().text = "";
        gameObject.GetComponent<AIDestinationSetter>().enabled = false;
        for (int i = 0; i < strength; i++)
        {
            if (moldieInventory[i] != null)
            {
                inventoryPos.GetComponent<InventoryManager>().inventory.Add(moldieInventory[i]);
                moldieInventory[i] = null;
            }

        }
    }

    //void NewItemInInvUI()
    //{
    //    foreach (Ingredient foodname in moldieInventory)
    //    {
    //        if(foodname != null)
    //        {
    //            itemName.GetComponent<TextMeshProUGUI>().text += foodname.ingredientName.ToString() + "\n";
    //        }
    //        else
    //        {
    //            continue;
    //        }
    //    }

    //}

    void ShowStats()
    {
        canvas2.SetActive(true);
        statsText.GetComponent<TextMeshProUGUI>().text = "Knowledge: " + knowledge.ToString() + "\n" + "Strength: " + strength.ToString() + "\n" + "Endurance: " + endurance.ToString() + "\n" + "Satisfaction: " + satisfaction.ToString() + "\n" + "Hunger: " + hunger.ToString() + "/" + maxHunger.ToString();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && showStats == false)
        {
            showStats = true;
        }

    }

    // MOLDIE-FRESS-FUNKTION
    public void FeedMoldie(Ingredient eatingIngredient)
    {
        Debug.Log(name + " vorheriger Hunger: " + hunger);
        Debug.Log(name + " frisst gleich " + eatingIngredient);

        if (CheckIfRawFood(eatingIngredient))
        {
            if (rawFoodEaten >= 3)
            {
                //Das gedragte Essen soll nicht verspeist werden
                return;
            }
            else
            {
                rawFoodEaten += 1;
            }
        }

        // Das neue Ingredient abrufen
        float satiation = eatingIngredient.satiation;
        //Hier muss noch die Zufriedenheits-Zunahme rein!

        // Hier dann deine Berechnung durchf�hren
        hunger += Mathf.RoundToInt(satiation);
        Debug.Log(name + " neuer Hunger: " + hunger);

        if (hunger > maxHunger)
        {
            garbageLoot.GetComponent<GarbagePile>().wasteCounter += eatingIngredient.garbage;
        }

        FindObjectOfType<CookingView>().RemoveItem(eatingIngredient);
    }

    bool CheckIfRawFood(Ingredient eatingIngredient)
    {
        if (eatingIngredient.type == IngredientType.CerealProduct
            || eatingIngredient.type == IngredientType.Vegetable
            || eatingIngredient.type == IngredientType.Cheese
            || eatingIngredient.type == IngredientType.Dairy
            || eatingIngredient.type == IngredientType.Nut)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}