using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "Master";
    public Slider slider;

    void Start()
    {
        float linear = PlayerPrefs.HasKey(parameterName) ? PlayerPrefs.GetFloat(parameterName) : 1f;

        mixer.SetFloat(parameterName, dB(linear));
        slider.value = linear;

        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat(parameterName, dB(value));

        PlayerPrefs.SetFloat(parameterName, value);
    }

    private float dB(float value) { return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f; }
}