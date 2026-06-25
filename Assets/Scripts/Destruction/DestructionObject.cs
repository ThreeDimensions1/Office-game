using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestructionObject : MonoBehaviour
{
    [Header("Score related")]
    // [SerializeField] protected int scoreGain = 100;
    // public int ScoreGain => scoreGain;
    [SerializeField] protected ScoreProfile scoreProfile = ScoreProfile.Small;
    [SerializeField] protected string displayName;
    public string DisplayName => displayName;

    [Header("Object settings")]
    public float destroyVelocity = 2;
    public UnityEvent onDestroy;

    [Header("Effects")]
    public float impulseForce = 1f;

    private bool isDestroyed = false;
    Rigidbody rb;

    CinemachineImpulseSource impulse;

    AudioSource source;

    public virtual void Start() {
        rb = GetComponent<Rigidbody>();
        impulse = FindAnyObjectByType<CinemachineImpulseSource>();
        source = GetComponentInChildren<AudioSource>();
    }

    public virtual void OnHit() {
        if (isDestroyed) return;
        impulse?.GenerateImpulseWithForce(impulseForce);
        if(source)
        {
            source?.Play();
        }
        isDestroyed = true;
        onDestroy?.Invoke();

        ScoreManager.Instance?.RegisterDestruction(scoreProfile, displayName);
    }
    
    protected virtual void OnCollisionEnter(Collision collision) {
        float velocity = collision.relativeVelocity.magnitude; //rb.linearVelocity.magnitude;

        if(velocity > destroyVelocity) {
            if(!isDestroyed && collision.gameObject.TryGetComponent(out FirstDestroy destroy) && !destroy.destroyed)
            {
                destroy.StartCrazyness();
            }
            OnHit();
        }
    }
}
