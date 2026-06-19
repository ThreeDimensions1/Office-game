using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController controller;
    private Rigidbody rb;

    public Transform Orientation;

    [Header("Raycast:")]
    public Transform KickDirection;
    public LayerMask LayersToKick;

    [Header("Kick:")]
    public float trueKickForce;
    public float relativeKickForce;
    public float nonkickForce;
    public float torqueForce = 20f;

    void Awake() {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        controller.inputJump += ProcessJump;
    }
    void OnDisable() {
        if (controller != null) {
            controller.inputJump -= ProcessJump;
        }
    }

    private void ProcessJump(bool obj) {
        if(obj == false) return;

        Ray kickRay = new Ray(KickDirection.position, KickDirection.forward);
        if(Physics.Raycast(kickRay, out RaycastHit hit, 1000f, LayersToKick)) {
            Vector3 force = new Vector3();
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
        } else {
            // If your not a gamer you dont get to kick like a man
            rb.AddForce(-KickDirection.forward * nonkickForce);
        }
    }
}