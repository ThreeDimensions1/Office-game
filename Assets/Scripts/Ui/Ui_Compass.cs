using UnityEngine;
using UnityEngine.UI;

public class Ui_Compass : MonoBehaviour
{
    public Transform Direction;
    public Image TorqueVisual;
    public Image SpeedVisual;

    Rigidbody playerRb;
    Camera cam;

    void Start() {
        playerRb = PlayerMovement.Instance.rb;
        cam = Camera.main;
    }

    void Update() {
        Vector3 cameraDir = cam.transform.forward;
        cameraDir.y = 0;
        cameraDir.Normalize();

        Vector3 playerDir = playerRb.transform.forward;

        float angle = Vector3.SignedAngle(playerDir, cameraDir, Vector3.up);
        Direction.localRotation = Quaternion.Euler(0, 0, angle);


        float currentTorque = playerRb.angularVelocity.y; 
        float maxTorque = 20f; // Used to calculate the 0-1 fill amount, its in Rad/sec jackass

        // 6. Set fill amount based on absolute torque intensity
        TorqueVisual.fillAmount = Mathf.Abs(currentTorque) / maxTorque;

        // 7. Toggle Counter-Clockwise fill direction based on whether torque is positive or negative
        if (currentTorque >= 0){
            TorqueVisual.fillClockwise = true;
        } else {
            TorqueVisual.fillClockwise = false;
        }
    }
}
