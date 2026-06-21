using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestructionObject : MonoBehaviour
{
    public float destroyVelocity = 2;
    public UnityEvent onDestroy;

    protected float velocity;

    Rigidbody rb;

    public virtual void Start()
    {
        PlayerDestruction.Instance.onHit += OnHit;
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnHit(DestructionObject obj, float velocity)
    {
        if(obj == this && velocity > destroyVelocity)
        {
            OnHit();
        }
    }

    public virtual void OnHit()
    {
        onDestroy?.Invoke();
    }

    void FixedUpdate()
    {
        velocity = rb.linearVelocity.magnitude;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(velocity > destroyVelocity)
        {
            OnHit();
        }
    }
}
