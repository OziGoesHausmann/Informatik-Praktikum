using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private CookingView cookingView;

    [Header("Slot Stats")]
    public IngredientType slotType;
    public Ingredient slottedIngredient;
    public Image slotIcon;
    public string slotName;
    public int slotDurability;
    public float slotSatiation;
    public int slotGarbage;

    public GameObject selectionBorder;
    public GameObject cross;

    public bool isSelected;
    public bool isNotUsable ;

    void Start()
    {
        if (slottedIngredient != null)
        {
            slotName = slottedIngredient.ingredientName;
            slotType = slottedIngredient.type;
            slotDurability = slottedIngredient.durability;
            slotSatiation = slottedIngredient.satiation;
            slotGarbage = slottedIngredient.garbage;
            slotIcon.sprite = slottedIngredient.icon;

            cookingView = GetComponentInParent<CookingView>();
        }
    }

    public void SelectSlot()
    {
        if (isNotUsable)
        {
            return;
        }

        cookingView.SlotSelected(this);
    }

    public void Selected()
    {
        isSelected = true;
        selectionBorder.SetActive(isSelected);
    }

    public void Deselected()
    {
        isSelected = false;
        selectionBorder.SetActive(isSelected);
    }

    public void SlotUsable()
    {
        isNotUsable = false;
        cross.SetActive(isNotUsable);
    }

    public void SlotUnusable()
    {
        isNotUsable = true;
        cross.SetActive(isNotUsable);
    }
}
