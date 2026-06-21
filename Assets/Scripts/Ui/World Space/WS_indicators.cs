using UnityEngine;
using UnityEngine.UI;

public class WS_indicators : MonoBehaviour
{
    public PlayerManager Manager;
    private PlayerMovement playerMovement;

    public Image[] KickIndicator;
    
    void Start() {
        playerMovement = PlayerMovement.Instance;
    }

    void Update() {
        transform.forward = playerMovement.transform.forward;
        transform.position = new Vector3(playerMovement.transform.position.x ,transform.position.y, playerMovement.transform.position.z);

        foreach(Image indicator in KickIndicator) {
            indicator.fillAmount = playerMovement.KickCharge;
        }
    }
}
