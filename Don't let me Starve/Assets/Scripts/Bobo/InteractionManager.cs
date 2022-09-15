using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private CharacterSlot[] allCharacters;

    public CharacterSlot selectedCharacterSlot;

    private void Start()
    {     
        allCharacters = FindObjectsOfType<CharacterSlot>();
    }

    public void Learn()
    {
        selectedCharacterSlot.Learning();
    }

    public void Train()
    {
        selectedCharacterSlot.Training();
    }

    public void Interact()
    {
        selectedCharacterSlot.Interacting();
    }

    public void Sleep()
    {
        foreach (CharacterSlot character in allCharacters)
        {
            character.Sleeping();
        }
    }

    public void Cook()
    {
        selectedCharacterSlot.Cooking();
    }

    public void SelectSlot(CharacterSlot currentSlot)
    {
        if (selectedCharacterSlot == null)
        {
            selectedCharacterSlot = currentSlot;
            return;
        }
        else
        {
            if (selectedCharacterSlot == currentSlot)
            {
                selectedCharacterSlot.SlotDeslected();
                selectedCharacterSlot = null;
                return;
            }
            else
            {
                selectedCharacterSlot.SlotDeslected();
                selectedCharacterSlot = currentSlot;
                return;
            }
        }
    }
}
