using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : Singleton<ParticlesManager>
{
    public void SpawnParticle(GameObject particle, Vector3 pos, Quaternion rot)
    {
        Instantiate(particle, pos,rot);
    }
}
