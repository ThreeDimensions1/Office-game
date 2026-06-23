using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaperButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool active = true;
    public Color onUp, onDown, onHighlight, onInactive;
    public UnityEvent onClick;

    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = active ? onUp : onInactive;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(active)
        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(active)
        rend.color = onDown;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(active)
        rend.color = onHighlight;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(active)
        rend.color = onHighlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(active)
        rend.color = onUp;
    }
}
