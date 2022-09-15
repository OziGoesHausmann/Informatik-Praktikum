using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingActivator : MonoBehaviour
{
    public GameObject cookingUI;
    public GameObject parent;
    public bool UIactivated;

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Moldie" && parent.GetComponent<CookingViewOpener>().charName == collision.name)
        {
            cookingUI.SetActive(true);
            UIactivated = true;
            Time.timeScale = 0;
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIactivated)
        {
            Invoke("ColliderDeactivator", 0.1f);
            cookingUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void ColliderDeactivator()
    {
        gameObject.SetActive(false);
    }
}
