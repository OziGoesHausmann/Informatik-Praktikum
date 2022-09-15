using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootList : MonoBehaviour
{
    
    public List<Ingredient> normal;
    public List<Ingredient> rare;
    public List<Ingredient> veryRare;
    public bool playerWasThere;
    // Start is called before the first frame update
    void Start()
    {
        playerWasThere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerWasThere == true)
        {
            deleteNullObjectsFromNormal();
            deleteNullObjectsFromRare();
            deleteNullObjectsFromSuperRare();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Moldie")
        {
            playerWasThere = true;
        }
    }

    void deleteNullObjectsFromNormal()
    {
        for (int i = 0; i < normal.Count; i++)
        {
            if (normal[i] == null)
            {
                normal.RemoveAt(i);
            }

        }
    }

    void deleteNullObjectsFromRare()
    {
        for (int i = 0; i < rare.Count; i++)
        {
            if (rare[i] == null)
            {
                rare.RemoveAt(i);
            }

        }
    }

    void deleteNullObjectsFromSuperRare()
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
