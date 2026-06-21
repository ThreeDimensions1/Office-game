using UnityEngine;
using UnityEngine.UI;

public class Ui_KickForceDisplay : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public Image[] KickIndicator;
    
    void Start() {
        playerMovement = PlayerMovement.Instance;
    }

    void Update() {
        foreach(Image indicator in KickIndicator) {
            indicator.fillAmount = playerMovement.KickCharge;
        }
    }
}
