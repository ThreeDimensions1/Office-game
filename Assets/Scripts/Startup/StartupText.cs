using TMPro;
using UnityEngine;

public class StartupText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = $"Starting {Application.productName}...";
    }
}
