using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Stat Data/New Item", order = 2)]

public class StatData : ScriptableObject
{
    public int levels = 3;
    public float[] values;
    public int[] prices;
}
