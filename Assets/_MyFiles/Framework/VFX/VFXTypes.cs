using System;
using UnityEngine;

[Serializable]
public struct ParticleSystemSpec
{
    public ParticleSystem particleSystem;
    public float size;

    public ParticleSystemSpec(ParticleSystem particleSystem, float size)
    { 
        this.size = 1f;
        this.particleSystem = null;
    }
}
