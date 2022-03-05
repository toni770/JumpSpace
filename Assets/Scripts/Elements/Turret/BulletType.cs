using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "BulletType/New Bullet", order = 1)]
public class BulletType : ScriptableObject
{
    public int id;
    public float shootSpeed;
    public float firstShootDelay;

    public float strength;
}
