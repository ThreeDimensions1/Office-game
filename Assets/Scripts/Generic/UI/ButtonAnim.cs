using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float animationSeconds = 0.5f;
    [SerializeField] AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 1.3f);
    bool onHover;
    float time;
    Vector3 startScale;
    RectTransform rectTransform;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHover = false;
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startScale = rectTransform.localScale;
    }

    void Update()
    {
        time = Mathf.Clamp01(onHover ? time + Time.unscaledDeltaTime / animationSeconds : time - Time.unscaledDeltaTime / animationSeconds);
        rectTransform.localScale = startScale * scaleCurve.Evaluate(time);
    }
}
