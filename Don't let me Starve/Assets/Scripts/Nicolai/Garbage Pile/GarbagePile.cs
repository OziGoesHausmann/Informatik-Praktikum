using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbagePile : MonoBehaviour
{
    //private List<Ingredient> sedimented;
    //private List<Ingredient> spoiled;
    
    //currently only this list is used!
    public List<Ingredient> normal;
    public List<Ingredient> rare;
    public List<Ingredient> veryRare;
    public bool playerWasThere;


    [SerializeField]
    public int wasteCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        normal = new List<Ingredient>();
        rare = new List<Ingredient>();
        veryRare = new List<Ingredient>();
        playerWasThere = false;
    }

    private void Update()
    {
        if (playerWasThere == true)
        {
            DeleteNullObjectsFromNormal();
            DeleteNullObjectsFromRare();
            DeleteNullObjectsFromSuperRare();
        }
    }

    public void AddFresh(Ingredient ingredient)
    {
        if (ingredient.rarity == Rarity.Normal)
            normal.Add(ingredient);

        if (ingredient.rarity == Rarity.Rare)
            rare.Add(ingredient);

        if(ingredient.rarity == Rarity.VeryRare)
            veryRare.Add(ingredient);

        Debug.Log("Should have Added " + ingredient.ingredientName + " to the garbage pile (Rarity: " + ingredient.rarity.ToString());
    }

    public void DecreaseDurabilities()
    {
        List <Ingredient> toSediment = new List<Ingredient>();
        
        //normal
        foreach (Ingredient ingredient in normal)
        {
            ingredient.durability -= 1;

            if (!ingredient.spoiled && ingredient.durability == 0)
            {
                ingredient.spoiled = true;

                if (ingredient.spoilType == IngredientSpoilType.Mushy)
                {
                    ingredient.durability = 3;
                    ingredient.status = IngredientStatus.Mushy;
                }
            }
            if (ingredient.spoiled && ingredient.durability == 0)
            {
                toSediment.Add(ingredient);
            }
        }

        foreach (Ingredient ingredient in toSediment)
        {
            normal.Remove(ingredient);
            wasteCounter += ingredient.garbage;
        }

        toSediment = new List<Ingredient>();

        //rare
        foreach (Ingredient ingredient in rare)
        {
            ingredient.durability -= 1;

            if (!ingredient.spoiled && ingredient.durability == 0)
            {
                ingredient.spoiled = true;

                if (ingredient.spoilType != IngredientSpoilType.Sediment)
                {
                    ingredient.durability = 3;
                }
            }
            if (ingredient.spoiled && ingredient.durability == 0)
            {
                toSediment.Add(ingredient);
            }
        }

        foreach (Ingredient ingredient in toSediment)
        {
            rare.Remove(ingredient);
            wasteCounter += ingredient.garbage;
        }

        toSediment = new List<Ingredient>();

        //very rare
        foreach (Ingredient ingredient in veryRare)
        {
            ingredient.durability -= 1;

            if(!ingredient.spoiled && ingredient.durability == 0)
            {
                ingredient.spoiled = true;

                if (ingredient.spoilType != IngredientSpoilType.Sediment)
                {
                    ingredient.durability = 3;
                }
            }
            if(ingredient.spoiled && ingredient.durability == 0)
            {
                toSediment.Add(ingredient);
            }
        }

        foreach (Ingredient ingredient in toSediment)
        {
            veryRare.Remove(ingredient);
            wasteCounter += ingredient.garbage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Moldie")
        {
            playerWasThere = true;
        }
    }

    void DeleteNullObjectsFromNormal()
    {
        for (int i = 0; i < normal.Count; i++)
        {
            if (normal[i] == null)
            {
                normal.RemoveAt(i);
            }

        }
    }

    void DeleteNullObjectsFromRare()
    {
        for (int i = 0; i < rare.Count; i++)
        {
            if (rare[i] == null)
            {
                rare.RemoveAt(i);
            }

        }
    }

    void DeleteNullObjectsFromSuperRare()
    {
        for (int i = 0; i < veryRare.Count; i++)
        {
            if (veryRare[i] == null)
            {
                veryRare.RemoveAt(i);
            }

        }
    }
}
