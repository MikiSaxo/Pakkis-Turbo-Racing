using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.VFX;

public class VFXfrontSpeed : MonoBehaviour
{
    public Rigidbody Rb;
    public ParticleSystem Ps; 

    [SerializeField] float minParticlesSpeedThreshold = 12;
    [SerializeField] float maxParticlesSpeedThreshold = 16;
    [SerializeField] int maxParticles = 200;

    float maxInterval = 0;

    private void Start()
    {
        maxInterval = maxParticlesSpeedThreshold - minParticlesSpeedThreshold;
    }

    [System.Obsolete]
    private void Update()
    {
        var emission = Ps.emission;
        //Debug.Log(Rb.velocity.magnitude);

        float currentSpeed = Rb.velocity.magnitude;
        float intervalSpeedValue = currentSpeed - minParticlesSpeedThreshold;
        if (intervalSpeedValue < 0)
            intervalSpeedValue = 0;

        float currentParticlesNumber = intervalSpeedValue * maxParticles / maxParticlesSpeedThreshold;
        //currentParticlesNumber = (int)currentParticlesNumber;

        //Timed bursts
        //Ps.emissionRate = currentParticlesNumber;

        // print(currentParticlesNumber);

        emission.rateOverTime = currentParticlesNumber;
    }




}
