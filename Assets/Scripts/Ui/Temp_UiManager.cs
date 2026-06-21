using UnityEngine;

public class Temp_UiManager : MonoBehaviour
{
    public PlayerManager manager;
    public GameObject[] TPV, FPV;

    void Update() {
        switch (manager.currentState) {
            case PlayerManager.State.FPV:
                Toggle(TPV, false);
                Toggle(FPV, true);
            break;
            case PlayerManager.State.TPV:
                Toggle(TPV, true);
                Toggle(FPV, false);
            break;
        }
    }
    void Toggle(GameObject[] objects, bool state) {
        foreach(GameObject uwu in objects) {
            uwu.SetActive(state);
        }
    }
}
