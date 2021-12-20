using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Item Data/New Item", order = 1)]
public class ItemData : ScriptableObject
{
    public int price;
    public Sprite image;

}
