using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    public string linkUrl;
    Button but;

    private void Start()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(OpenButtonLink);
    }

    void OpenButtonLink()
    {
        if (linkUrl != string.Empty && linkUrl.Contains("http"))
        {
            Application.OpenURL(linkUrl);
        }
    }
}
