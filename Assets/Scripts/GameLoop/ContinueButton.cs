using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<TMP_Text>().text = $"Continue - {FloorInfo.Instance.nextFloorLvlName}";
    }
    public void Continue()
    {
        SceneManager.LoadScene(FloorInfo.Instance.nextFloorSceneName);
    }
}
