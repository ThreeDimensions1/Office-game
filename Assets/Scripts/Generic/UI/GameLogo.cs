using UnityEngine;
using UnityEngine.UI;

public class GameLogo : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("icon");
    }
}
