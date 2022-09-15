using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiningModus : MonoBehaviour
{
    public bool modusOn;
    public GameObject modusUI;
    private ScreenWipe screenWipe;
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject char4;
    public TMP_Text dayCounter;
    public int daysPast;

    private CookingView cookingView;

    private void Awake()
    {
        screenWipe = FindObjectOfType<ScreenWipe>();
        cookingView = modusUI.GetComponent<CookingView>();
    }

    private void Start()
    {
        daysPast = 1;
    }

    void Update()
    {

        //dayCounter.text = "Day: " + daysPast.ToString();

        if (modusOn)
        {

            Time.timeScale = 0;
            modusUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            modusUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I) && !modusOn)
        {
            modusOn = true;
            cookingView.UpdateUI();
            //Debug.Log("ON");
        }
        else if(Input.GetKeyDown(KeyCode.I) && modusOn)
        {
            modusOn = false;
            //Debug.Log("OFF");
        }
    }

    public void ButtonClick()
    {
        modusOn = false;
    }

    public void InitiateNextDay()
    {
        Invoke("TextDelayed", 2f);
        screenWipe.ToggleWipe(true);
        screenWipe.isDone = true;
        char1.GetComponent<MoldieInteraction>().nextDayStarted = true;
        char2.GetComponent<MoldieInteraction>().nextDayStarted = true;
        char3.GetComponent<MoldieInteraction>().nextDayStarted = true;
        char4.GetComponent<MoldieInteraction>().nextDayStarted = true;
    }

    public void TextDelayed()
    {
        daysPast += 1;
    }



}
