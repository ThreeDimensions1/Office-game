using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioMixerGroup group;
    [Space]
    public AudioClip hover;
    public AudioClip click;


    AudioSource source;
    private void Awake()
    {
        GameObject obj = new GameObject($"{name}'s audio source");
        obj.transform.parent = transform;
        source = obj.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = group;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        source.clip = hover;
        source.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.clip = click;
        source.Play();
    }
}
