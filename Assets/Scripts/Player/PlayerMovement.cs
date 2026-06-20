using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    private PlayerController controller;
    public Rigidbody rb {get; private set; }

    public Transform Orientation;

    [Header("Raycast & Boxcast:")]
    public Transform KickDirection;
    public Vector3 boxScale = new Vector3(1f, 1f, 1f);
    public LayerMask LayersToKick;

    [Header("Kick:")]
    public float trueKickForce;
    public float relativeKickForce;
    public float nonkickForce;
    public float torqueForce = 20f;

    [Header("Rotation Handling")]
    public bool thirdPerson;
    public Vector2 spinRandomness = new(5, 10);
    
    [Header("Speed limits")]
    [Min(0)] public float speedLimit = 0;

    Vector2 movementVector;

    const float deadzone = 0.2f;

    void Awake() {
        if(Instance) {
            Debug.LogError("Movement instance already set");
            Destroy(gameObject);
        }

        Instance = this;

        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        if(speedLimit > 0)
        {
            rb.maxLinearVelocity = speedLimit;
        }
    }

    void OnEnable() {
        controller.inputJump += ProcessJump;
    }
    void OnDisable() {
        if (controller != null) {
            controller.inputJump -= ProcessJump;
        }
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(controller.MovementInput.x) > deadzone && movementVector.x != controller.MovementInput.x)
        {
            rb.AddTorque(0, UnityEngine.Random.Range(spinRandomness.x, spinRandomness.y) * controller.MovementInput.x, 0, ForceMode.Impulse);
        }
        movementVector = controller.MovementInput;
    }

    private void ProcessJump(bool obj) {
        if(!obj) return;

        if(thirdPerson) ProcessJump3Person();
        else ProcessJump1Person();
    }

    void ProcessJump1Person() {
        Ray kickRay = new Ray(KickDirection.position, KickDirection.forward);

        if(Physics.Raycast(kickRay, out RaycastHit hit, 1000f, LayersToKick)) {
            ForceAndTorque(hit);
        } else {
            // If your not a gamer you dont get to kick like a man
            rb.AddForce(-KickDirection.forward * nonkickForce, ForceMode.Impulse);
        }
    }
    void ProcessJump3Person() {
        // rb.AddForce(-transform.forward * trueKickForce, ForceMode.Impulse);
        if(Physics.BoxCast(Orientation.position, boxScale/2, Orientation.forward, out RaycastHit hit, Orientation.rotation, 1000f, LayersToKick)){
            ForceAndTorque(hit);
        } else {
            // If your not a gamer you dont get to kick like a man
            rb.AddForce(-transform.forward * nonkickForce, ForceMode.Impulse);
        }
    }
    void ForceAndTorque(RaycastHit hit) {
        Vector3 force = new();

        // 1. THE FUNNY FORCE

        // absolute push
        force += -KickDirection.forward * trueKickForce;
        // funny push
        force += new Vector3(hit.normal.x, 0, hit.normal.z).normalized * relativeKickForce;

        rb.AddForce(force, ForceMode.Impulse);

        // 2. THE FUNNY TORQUE

        // Calculate the rotational axis perpendicular to the kick and the wall normal
        Vector3 torqueAxis = Vector3.Cross(KickDirection.forward, hit.normal);
        rb.AddTorque(torqueAxis * torqueForce, ForceMode.Impulse);
    }
    
    private void OnDrawGizmosSelected() {
        if (Orientation == null) return;

        if (thirdPerson) {
            Gizmos.color = Color.red;
            // This draws the starting box position
            Gizmos.matrix = Matrix4x4.TRS(Orientation.position, Orientation.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, boxScale);
        }
    }
}