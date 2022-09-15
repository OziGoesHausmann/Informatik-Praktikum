using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CookingView : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public RectTransform slotParent;
    public RectTransform slotPrefab;

    [Header("Header")]
    public TMP_Text categoryName;
    string currentCategory;
    public Sprite sortingArrowUp;
    public Sprite sortingArrowDown;
    public Image nameArrow;
    public Image durabilityArrow;
    public Image nameButton;
    public Image durabilityButton;
    public Color activeColor;

    [Header("Detail Screen")]
    public InventorySlot currentSelection;
    public Image detailIcon;
    public TMP_Text detailName;
    public TMP_Text detailCategory;
    public TMP_Text detailDurability;
    public TMP_Text detailSatiation;
    public TMP_Text detailGarbage;
    public GameObject detailView;

    [Header("Sorting")]
    public bool sortAscending;
    public bool sortName;  
    public bool categoryView;

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    void Start()
    {
        Invoke("SortDefault", 0.1f);
    }

    public void SlotSelected(InventorySlot selectedItemSlot)
    {
        if (selectedItemSlot != currentSelection)
        {
            if (currentSelection != null)
            {
                currentSelection.Deselected();
            }

            detailView.SetActive(true);
            
            currentSelection = selectedItemSlot;

            currentSelection.Selected();

            FillDetailView();
        }
        else
        {
            detailView.SetActive(false);
            
            currentSelection.Deselected();

            currentSelection = null;

            return;
        }
    }

    void FillDetailView()
    {
        detailIcon.sprite = currentSelection.slotIcon.sprite;
        detailName.text = currentSelection.slotName;
        detailCategory.text = currentSelection.slotType.ToString();
        detailDurability.text = currentSelection.slotDurability.ToString();
        detailSatiation.text = currentSelection.slotSatiation.ToString();
        detailGarbage.text = currentSelection.slotGarbage.ToString();
    }

    public void SortDefault()
    {
        Debug.Log("No Filter");

        sortName = true;
        sortAscending = true;
        categoryView = false;

        currentCategory = "All Items";

        SpawnInventory();
    }

    public void UpdateUI()
    {
        SpawnInventory();
    }

    public void SortByName()
    {
        if (!sortName)
        {
            durabilityButton.color = Color.white;
            nameButton.color = activeColor;

            durabilityArrow.enabled = false;
            nameArrow.enabled = true;
            sortName = true;
        }
        else
        {
            sortAscending = !sortAscending;
        }

        if (sortAscending)
        {
            nameArrow.sprite = sortingArrowUp;
        }
        else
        {
            nameArrow.sprite = sortingArrowDown;
        }

        SpawnInventory();
    }

    public void SortByDurability()
    {
        if (sortName)
        {
            nameButton.color = Color.white;
            durabilityButton.color = activeColor;

            nameArrow.enabled = false;
            durabilityArrow.enabled = true;
            sortName = false;
        }
        else
        {
            sortAscending = !sortAscending;
        }

        if (sortAscending)
        {
            durabilityArrow.sprite = sortingArrowUp;
        }
        else
        {
            durabilityArrow.sprite = sortingArrowDown;
        }

        SpawnInventory();
    }

    private void Sorting()
    {
        if (sortName)
        {
            if (sortAscending)
            {
                SortByNameAscend();
            }
            else
            {
                SortByNameDescend();
            }
        }
        else
        {
            if (sortAscending)
            {
                SortByDurabilityAscend();
            }
            else
            {
                SortByDurabilityDescend();
            }
        }
    }

    private void SortByNameAscend()
    {
        Debug.Log("Sort by Name Ascending");
        inventoryManager.inventory = inventoryManager.inventory.OrderBy(x => x.ingredientName).ToList();
        inventoryManager.inventoryCategorised = inventoryManager.inventoryCategorised.OrderBy(x => x.ingredientName).ToList();
    }

    private void SortByNameDescend()
    {
        Debug.Log("Sort by Name Descending");
        inventoryManager.inventory = inventoryManager.inventory.OrderByDescending(x => x.ingredientName).ToList();
        inventoryManager.inventoryCategorised = inventoryManager.inventoryCategorised.OrderByDescending(x => x.ingredientName).ToList();
    }

    private void SortByDurabilityAscend()
    {
        Debug.Log("Sort by Durability Ascending");
        inventoryManager.inventory = inventoryManager.inventory.OrderBy(x => x.durability).ToList();
        inventoryManager.inventoryCategorised = inventoryManager.inventoryCategorised.OrderBy(x => x.durability).ToList();
    }

    private void SortByDurabilityDescend()
    {
        Debug.Log("Sort by Durability Descending");
        inventoryManager.inventory = inventoryManager.inventory.OrderByDescending(x => x.durability).ToList();
        inventoryManager.inventoryCategorised = inventoryManager.inventoryCategorised.OrderByDescending(x => x.durability).ToList();
    }

    /*
    public void SpawnInventoryCategorised(string selectedCategory)
    {
        IngredientType selectedIngredientType = (IngredientType)System.Enum.Parse(typeof(IngredientType), selectedType);
        Debug.Log("Filter: " + selectedIngredientType);

        inventoryManager.CategorisedInventory(selectedIngredientType);
        currentCategory = selectedCategory;
        categoryView = true;

        detailView.SetActive(false);

        SpawnInventory();
    }
    */

    public void SpawnInventoryCategorised(string selectedCategory)
    {
        Debug.Log("Filter: " + selectedCategory);

        inventoryManager.CategorisedInventory(selectedCategory);
        currentCategory = selectedCategory;
        categoryView = true;

        SpawnInventory();
    }

    private void SpawnInventory()
    {
        if (slotParent.childCount > 0)
        {
            EmptyInventory();
        }

        Sorting();

        List<Ingredient> ingredients = new List<Ingredient>();

        if (!categoryView)
        {
            ingredients = inventoryManager.inventory;
        }
        else
        {
            ingredients = inventoryManager.inventoryCategorised;
        }

        int i = 0;

        foreach (Ingredient ingredient in ingredients)
        {
            RectTransform slotInstance = Instantiate(slotPrefab, slotParent);
            slotInstance.name = "Slot_" + i;
            i++;
            slotInstance.GetComponent<InventorySlot>().slottedIngredient = ingredient;
        }

        categoryName.text = currentCategory;
        detailView.SetActive(false);

        /*
        if(FindObjectOfType<IngredientDrop>()._ingredients.Count != 0)
        {
            FindObjectOfType<IngredientDrop>().UpdateAddableIngredients();
        }
        */
    }

    public void EmptyInventory()
    {
        foreach (RectTransform child in slotParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddItem(Ingredient newItem)
    {
        inventoryManager.inventory.Add(newItem);
        SpawnInventory();
    }

    public void RemoveItem(Ingredient newItem)
    {
        inventoryManager.inventory.Remove(newItem);
        detailView.SetActive(false);

        SpawnInventory();
    }

    public void FilterAddableIngredients(List<IngredientType> filterSpecificTypes, List<Ingredient> filterSpecificIngredients)
    {
        List<InventorySlot> inventorySlots = slotParent.GetComponentsInChildren<InventorySlot>().ToList();

        foreach (InventorySlot slot in inventorySlots)
        {
            foreach (Ingredient ingredient in filterSpecificIngredients)
            {
                if (slot.slottedIngredient.ingredientName == ingredient.ingredientName)
                {
                    slot.SlotUsable();
                    return;
                }
            }

            foreach (IngredientType ingredientType in filterSpecificTypes)
            {
                if (slot.slotType == ingredientType)
                {
                    slot.SlotUsable();
                    return;
                }
            }

            slot.SlotUnusable();
        }
    }
}