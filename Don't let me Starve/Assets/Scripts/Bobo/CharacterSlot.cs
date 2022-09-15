using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSlot : MonoBehaviour
{
    public Character character;

    string name;
    int strength;
    int knowledge;
    int cooking;
    int stamina;
    int maxStamina;

    public TMP_Text charName;
    public TMP_Text strengthValue;
    public TMP_Text knowledgeValue;
    public TMP_Text cookingValue;

    public Image charIcon;

    public List<Toggle> staminaPoints = new List<Toggle>();

    public RectTransform staminaPointParent;
    public Toggle staminaPointPrefab;

    public bool exhausted;

    private InteractionManager interactionManager;

    public bool slotSelected;
    public Image border;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();

        name = character.name;
        strength = character.strength;
        knowledge = character.knowledge;
        cooking = character.cooking;
        maxStamina = character.maxStamina;

        stamina = maxStamina;

        charIcon.sprite = character.icon;

        for (int i = 0; i < maxStamina; i++)
        {
            Toggle staminaPointInstance = Instantiate(staminaPointPrefab, staminaPointParent);
            staminaPoints.Add(staminaPointInstance);
        }
    }

    private void OnGUI()
    {
        charName.text = name;
        strengthValue.text = strength.ToString();
        knowledgeValue.text = knowledge.ToString();
        cookingValue.text = cooking.ToString();

        if (stamina < maxStamina)
        {
            for (int i = 0; i < stamina; i++)
            {
                staminaPoints[i].isOn = true;
            }

            for (int j = stamina; j < maxStamina; j++)
            {
                staminaPoints[j].isOn = false;
            }

        }
        else
        {
            foreach (Toggle staminaPoint in staminaPoints)
            {
                staminaPoint.isOn = true;
            }
        }

        if (slotSelected)
        {
            border.enabled = true;
        }
        else
        {
            border.enabled = false;
        }
    }

    public void Learning()
    {
        if (!StaminaCheck(maxStamina))
        {
            return;
        }

        knowledge += 1;
    }

    public void Training()
    {
        if (!StaminaCheck(maxStamina))
        {
            return;
        }

        strength += 1;
    }

    public void Cooking()
    {
        if (!StaminaCheck(1))
        {
            return;
        }

        cooking += 1;
    }

    public void Interacting()
    {
        if (!StaminaCheck(2))
        {
            return;
        }
    }

    public void Sleeping()
    {
        exhausted = false;

        stamina = maxStamina;
    }

    bool StaminaCheck(int cost)
    {
        if (cost <= stamina)
        {
            stamina -= cost;

            if (stamina <= 0)
            {
                exhausted = true;
            }

            return true;
        }
        else
        {
            Debug.Log(name + " is to exhausted for this work!");
            return false;
        }
    }

    public void SlotSelected()
    {
        slotSelected = true;
        interactionManager.SelectSlot(this);
    }

    public void SlotDeslected()
    {
        slotSelected = false;
    }
}
