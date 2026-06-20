using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
    private PlayerController controller;
    
    public Transform LookAt;
    public Vector2 RotationBoundaries;
    // public Vector2 RotationRatio;
    // public float Offset;
    public float mouseSensitivity;

    private Vector2 Rotation;

    void Awake() {
        controller = GetComponent<PlayerController>();
    }

    void Update() {
        /*Vector2 lookDelta = controller.RotationInput;

        float PositionX = LookAt.localPosition.x + lookDelta.x * mouseSensitivity;
        float PositionY = LookAt.localPosition.y + lookDelta.y * mouseSensitivity;

        PositionX = Mathf.Clamp(PositionX, -MovementRange.x, MovementRange.x);
        PositionY = Mathf.Clamp(PositionY, -MovementRange.y, MovementRange.y);

        LookAt.localPosition = new Vector3(PositionX, PositionY, LookAt.localPosition.z);*/

        Rotation += controller.RotationInput * mouseSensitivity;
        Rotation = new Vector2(Mathf.Clamp(Rotation.x, -RotationBoundaries.x, RotationBoundaries.x), Mathf.Clamp(Rotation.y, -RotationBoundaries.y, RotationBoundaries.y));

        LookAt.localRotation = Quaternion.Euler(-Rotation.y,Rotation.x,0);
    }

    void OnDrawGizmosSelected() {
        // Gizmos.DrawCube(LookAt.position, new Vector3)
    }
}