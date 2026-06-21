using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
    private PlayerController controller;
    
    public Transform LookAt;
    public Vector2 RotationBoundaries;
    // public Vector2 RotationRatio;
    // public float Offset;
    public float mouseSensitivity;

    [HideInInspector] public bool CameraLock = false;
    private bool cursorReleased;// = false;

    private Vector2 Rotation;

    void Awake() {
        controller = GetComponent<PlayerController>();

        CursorLock(false);
    }
    void OnEnable() {
        controller.inputCursorLock += CursorLock;
    }
    void OnDisable() {
        if (controller != null) {
            controller.inputCursorLock -= CursorLock;
        }
    }

    private void CursorLock(bool obj) {
        if (obj) {
            cursorReleased = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            cursorReleased = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update() {
        if(cursorReleased || CameraLock) return;

        Rotation += controller.RotationInput * mouseSensitivity;
        Rotation = new Vector2(Mathf.Clamp(Rotation.x, -RotationBoundaries.x, RotationBoundaries.x), Mathf.Clamp(Rotation.y, -RotationBoundaries.y, RotationBoundaries.y));

        LookAt.localRotation = Quaternion.Euler(-Rotation.y,Rotation.x,0);
    }

    void OnDrawGizmosSelected() {
        // Gizmos.DrawCube(LookAt.position, new Vector3)
    }
}