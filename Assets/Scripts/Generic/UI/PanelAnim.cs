using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnim : MonoBehaviour
{
    [SerializeField] float animationSeconds = 0.5f;
    [Header("Properties")]
    [SerializeField] bool useScale;
    [SerializeField] AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] bool useAlpha;
    [SerializeField] AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] bool useRotation;
    [SerializeField] AnimationCurve rotationCurve = AnimationCurve.Constant(0, 1, 0);
    [SerializeField] bool usePosition;
    [SerializeField] Vector3 defaultPosition = Vector3.zero;
    [SerializeField] Vector3 startPosition;
    [SerializeField] AnimationCurve positionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    RectTransform rectTransform;
    Vector3 startScale;
    Image image;
    float time;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startScale = transform.localScale;
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.unscaledDeltaTime / animationSeconds;
        if (useScale)
        {
            transform.localScale = startScale * scaleCurve.Evaluate(time);
        }
        if (useAlpha)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaCurve.Evaluate(time));
        }
        if (useRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationCurve.Evaluate(time));
        }
        if (usePosition)
        {
            rectTransform.anchoredPosition = LerpVector(startPosition, defaultPosition, positionCurve.Evaluate(time));
        }
    }

    Vector2 LerpVector(Vector2 a, Vector2 b, float t)
    {
        return new Vector2(LerpFloat(a.x, b.x, t), LerpFloat(a.y, b.y, t));
    }

    float LerpFloat(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}
