using System;
using UnityEngine;

public class PlayerDestruction : MonoBehaviour
{
    public static PlayerDestruction Instance;
    // keep this
    public Action<DestructionObject, float> onHit;
    // float velocity;
    // public TagHandle destructibles;
    Rigidbody rb;

    void Awake()
    {
        if(Instance)
        {
            Debug.Log("PlayerDestruction singleton already exists!");
            Destroy(gameObject);
        }
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    void OnDestroy()
    {
        Instance = null;
    }

    /*void FixedUpdate() {
        velocity = rb.linearVelocity.magnitude;
    }*/

    /*void OnCollisionEnter(Collision collision) {
        float velocity = rb.linearVelocity.magnitude;

        if(collision.gameObject.CompareTag(destructibles))
        if(collision.gameObject.TryGetComponent(out DestructionObject obj)) {
            // onHit?.Invoke(obj, velocity);
            obj.
        }
    }*/
}
