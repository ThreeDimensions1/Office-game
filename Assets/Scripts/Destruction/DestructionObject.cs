using System;
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

    private bool isDestroyed = false;
    Rigidbody rb;

    public virtual void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnHit() {
        if (isDestroyed) return;
        isDestroyed = true;
        onDestroy?.Invoke();

        ScoreManager.Instance.RegisterDestruction(scoreProfile, displayName);
    }
    
    protected virtual void OnCollisionEnter(Collision collision) {
        float velocity = collision.relativeVelocity.magnitude; //rb.linearVelocity.magnitude;

        if(velocity > destroyVelocity) {
            OnHit();
        }
    }
}
