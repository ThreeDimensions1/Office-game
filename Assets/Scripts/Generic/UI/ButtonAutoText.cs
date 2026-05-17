using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonAutoText : MonoBehaviour
{
    void Update()
    {
        if(TryGetComponent(out TMP_Text text) && !Application.isPlaying)
            text.text = GetComponentInParent<Button>().gameObject.name;
    }

    private void Awake()
    {
        if (TryGetComponent(out TMP_Text text))
            text.text = GetComponentInParent<Button>().gameObject.name;
    }
}