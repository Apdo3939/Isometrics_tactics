using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillVisualFX : MonoBehaviour
{
    public ParticleSystem onlyOntHit;
    public ParticleSystem always;
    public bool didHit;
    public UnitCharacter target;
    public Vector3 offset;
    public float delay;

    public void VFX()
    {
        Invoke("VFXDelay", delay);
    }

    void VFXDelay()
    {
        if (always != null)
        {
            SpawnEffect(always);
        }
        if (didHit && onlyOntHit != null)
        {
            SpawnEffect(onlyOntHit);
        }
    }

    void SpawnEffect(ParticleSystem PS)
    {
        Instantiate(PS, target.SS.transform.position + offset, Quaternion.identity, target.SS.transform);
    }
}
