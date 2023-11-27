using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXfrontSpeed : MonoBehaviour
{
    public Rigidbody Rb;
    public ParticleSystem Ps; 

    [SerializeField] float maxParticlesSpeedThreshold;
    [SerializeField] int maxParticles = 200;

    private void Update()
    {
        
    }




}
