using UnityEngine;

public class Ui_PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }
}
