using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
    private PlayerController controller;
    
    public Transform LookAt;
    public Vector2 MovementRange;
    // public float Offset;
    public float mouseSensitivity;

    void Awake() {
        controller = GetComponent<PlayerController>();
    }

    void Update() {
        Vector2 lookDelta = controller.RotationInput;

        float PositionX = LookAt.localPosition.x + lookDelta.x * mouseSensitivity;
        float PositionY = LookAt.localPosition.y + lookDelta.y * mouseSensitivity;

        PositionX = Mathf.Clamp(PositionX, -MovementRange.x, MovementRange.x);
        PositionY = Mathf.Clamp(PositionY, -MovementRange.y, MovementRange.y);

        LookAt.localPosition = new Vector3(PositionX, PositionY, LookAt.localPosition.z);
    }

    void OnDrawGizmosSelected() {
        // Gizmos.DrawCube(LookAt.position, new Vector3)
    }
}