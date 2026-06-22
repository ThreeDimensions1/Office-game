using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color onUp, onDown;
    public UnityEvent onClick;

    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rend.color = onDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rend.color = onUp;
    }
}
