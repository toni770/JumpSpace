using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Item/JetPack", order = 1)]
public class JetPack : ScriptableObject
{
    public int itemId;
    public int price;
    public Sprite image;

}
