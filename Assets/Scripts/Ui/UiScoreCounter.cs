using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiScoreCounter : MonoBehaviour
{
    public float lifetime;
    public TMP_Text text;

    public void UpdateContent(string content) {
        text.text = content;
    }
}