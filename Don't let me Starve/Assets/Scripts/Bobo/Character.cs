using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/New Character")]
public class Character : ScriptableObject
{
    public string name;

    public Sprite icon;

    public int strength;

    public int knowledge;

    public int maxStamina;

    public int cooking;
}
