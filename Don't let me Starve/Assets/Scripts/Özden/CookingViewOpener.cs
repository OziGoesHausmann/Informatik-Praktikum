using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingViewOpener : MonoBehaviour
{
    public GameObject raycastObject;
    public string charName;
    public GameObject selectedChar;
    public int enduranceMinus;

    private void Update()
    {
        
    }

    private void OnMouseOver()
    {
        selectedChar = raycastObject.GetComponent<Raycast>().selectedObject;

        if(Input.GetMouseButtonDown(0) && selectedChar.tag == "Moldie" && selectedChar.GetComponent<MoldieInteraction>().endurance >= enduranceMinus)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            selectedChar.GetComponent<MoldieInteraction>().endurance -= enduranceMinus; //Genauen Wert festlegen
            charName = selectedChar.name;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Moldie" && charName == )
    //    {
    //        transform.GetChild(0).gameObject.SetActive(false);
    //    }
    //}



}
