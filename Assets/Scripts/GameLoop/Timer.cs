using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int time = 60;
    public UnityEvent onCountEnd;
    TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        time = FloorInfo.Instance.currentFloor.gameTime;
        UpdateText();
    }

    public void StartCounting()
    {
        StartCoroutine(counter());
    }

    IEnumerator counter()
    {
        while(time > 0)
        {
            time -= 1;
            UpdateText(); 
            yield return new WaitForSeconds(1);
        }
        onCountEnd?.Invoke();
    }

    void UpdateText()
    {
        int minutes = time / 60;
        int seconds = time % 60;
        if (seconds == 0) text.text = $"{minutes}:{seconds}0";
        else if (seconds < 10) text.text = $"{minutes}:0{seconds}";
        else text.text = $"{minutes}:{seconds}";
    }
}
