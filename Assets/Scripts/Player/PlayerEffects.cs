using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Cinemachine impulse")]
    public float impulseStrenght = 0.3f;
    [Header("Sfx")]
    public AudioSource crashSource;
    public AudioSource rollingSource;
    public float maxChairSpeed = 15f;
    public Vector2 rollingPitchClamp = new(0.5f, 1);
    CinemachineImpulseSource impulse;
    Rigidbody rb;

    void Start()
    {
        impulse = FindAnyObjectByType<CinemachineImpulseSource>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(rollingSource && rb)
        {
            float val = Mathf.Clamp01(Math.Abs(rb.linearVelocity.magnitude / maxChairSpeed));
            rollingSource.pitch = Mathf.Lerp(rollingPitchClamp.x, rollingPitchClamp.y, val);
            rollingSource.volume = val;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        crashSource?.Play();
        impulse?.GenerateImpulseWithForce(impulseStrenght);
    }
}
