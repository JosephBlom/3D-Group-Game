using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public new string name = "New Item";
    public string description = "New Description";
    public Sprite icon;
    public int currentQuantity = 1;
    public int maxQuantity = 16;
    public int cost = 1;
    [Tooltip("Set this to the higher number in the drop chance (ex. 1 in a 1000, drop chance = 1000)")]
    public float dropChance;
}
