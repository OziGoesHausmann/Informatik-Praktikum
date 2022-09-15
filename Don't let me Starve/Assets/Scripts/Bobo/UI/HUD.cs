using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text currentDay;
    public TMP_Text currentWaste;

    public DiningModus diningModus;
    public WasteController wasteController;

    private void OnGUI()
    {
        currentDay.text = "Day: " + diningModus.daysPast;
        currentWaste.text = "Waste: " + wasteController.garbagePile.wasteCounter;
    }
}
