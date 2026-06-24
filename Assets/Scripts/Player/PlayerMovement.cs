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

    [Header("Charge up:")]
    public float KickCharge;
    [SerializeField] private float totalChargeTime = 2.0f;
    bool chargingKick = false;

    [Header("Kick:")]
    public float trueKickForce;
    public float relativeKickForce;
    public float nonkickForce;
    public float torqueForce = 20f;

    [Header("Destruction:")]
    public float destructionForceDirect;
    public float explosionForce;
    public float explostionSize;
    public float upwardsModifier;
    // public Transform reliableExplosionPoint;

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
        if(Mathf.Abs(controller.MovementInput.x) > deadzone && movementVector.x != controller.MovementInput.x) {
            rb.AddTorque(0, UnityEngine.Random.Range(spinRandomness.x, spinRandomness.y) * controller.MovementInput.x, 0, ForceMode.Impulse);
        }
        movementVector = controller.MovementInput;

        if (chargingKick) {
            KickCharge += Time.fixedDeltaTime / totalChargeTime;
            KickCharge = Mathf.Clamp01(KickCharge);
        }
    }

    private void ProcessJump(bool obj) {
        if (obj) {
            chargingKick = true;
        } else {
            chargingKick = false;

            if(thirdPerson) ProcessJump3Person();
            else ProcessJump1Person();

            KickCharge = 0f;
        }
    }

    void ProcessJump1Person() {
        Ray kickRay = new Ray(KickDirection.position, KickDirection.forward);

        float forceModifier = KickCharge;

        if(Physics.Raycast(kickRay, out RaycastHit hit, 1000f, LayersToKick)) {
            ForceAndTorque(hit, forceModifier);
            KickDestruction(hit, forceModifier, hit.point);
        } else {
            // If your not a gamer you dont get to kick like a man
            rb.AddForce(-KickDirection.forward * nonkickForce * forceModifier, ForceMode.Impulse);
        }
    }
    void ProcessJump3Person() {
        float forceModifier = KickCharge;

        // if(Physics.BoxCast(Orientation.position, boxScale/2, Orientation.forward, out RaycastHit hit, Orientation.rotation, 1000f, LayersToKick)){
        //     ForceAndTorque(hit, forceModifier);
        //     Vector3 reliableExplosionPoint = (hit.point == Vector3.zero) ? hit.collider.transform.position : hit.point;

        //     KickDestruction(hit, forceModifier, reliableExplosionPoint);
        // } else {
        //     // If your not a gamer you dont get to kick like a man
        //     rb.AddForce(-transform.forward * nonkickForce * forceModifier, ForceMode.Impulse);
        // }
        rb.AddForce(-transform.forward * nonkickForce * forceModifier, ForceMode.Impulse);
    }
    void ForceAndTorque(RaycastHit hit, float forceModifier) {
        Vector3 force = new();

        // 1. THE FUNNY FORCE

        // absolute push
        force += -KickDirection.forward * trueKickForce;
        // funny push
        force += new Vector3(hit.normal.x, 0, hit.normal.z).normalized * relativeKickForce;

        rb.AddForce(force * forceModifier, ForceMode.Impulse);

        // 2. THE FUNNY TORQUE

        // Calculate the rotational axis perpendicular to the kick and the wall normal
        Vector3 torqueAxis = Vector3.Cross(KickDirection.forward, hit.normal);
        rb.AddTorque(torqueAxis * torqueForce * forceModifier, ForceMode.Impulse);
    }
    void KickDestruction(RaycastHit hit, float forceModifier, Vector3 explosionOrigin) {
        // direct kick
        Rigidbody targetRb = hit.collider.GetComponent<Rigidbody>();
        if(targetRb == null) return;
        
        targetRb.AddForce(KickDirection.forward * destructionForceDirect * forceModifier, ForceMode.Impulse);
        // explosion
        if(explostionSize > 0) {
            Collider[] colliders = Physics.OverlapSphere(hit.point, explostionSize, LayersToKick);
            for(int i = 0; i < colliders.Length; i++) {
                targetRb = colliders[i].GetComponent<Rigidbody>();
                if(targetRb != null) {
                    // targetRb.AddExplosionForce(explosionForce * forceModifier, hit.point, explostionSize);
                    targetRb.AddExplosionForce(explosionForce * forceModifier, explosionOrigin, explostionSize, upwardsModifier, ForceMode.Impulse);
                }
            }
        }
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