using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "Clip/New Clip", order = 1)]
public class AudioConfig : ScriptableObject
{
    public AudioClip Clip;
    [Range (0,1)] public float Volume = 0.2f;
    [Range(0,2)]  public float Pitch = 1;
    public bool Loop = false;
    
}
