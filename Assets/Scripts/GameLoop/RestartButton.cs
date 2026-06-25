using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = $"Restart - {FloorInfo.Instance.currentFloor.floorName}";
    }
    public void Restart()
    {
        SceneManager.LoadScene(FloorInfo.Instance.currentFloor.sceneName);
    }
}
