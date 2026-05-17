using TMPro;
using UnityEngine;

public class GameTitle : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent(out TMP_Text text))
        {
            text.text = Application.productName;
        }
    }
}
