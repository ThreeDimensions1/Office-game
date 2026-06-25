using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class FirstDestroy : MonoBehaviour
{
    public bool destroyed {get; private set; }
    public AudioSource crowd;
    public AudioSource scream;
    public AudioSource metal;

    [Header("Cam noise")]
    public CinemachineBasicMultiChannelPerlin camNoise;
    public float amplitude;
    public float frequency;

    [Header("Animation")]

    public Animator animator;
    public string animationName;
    [Header("Others")]
    public Timer timer;

    public void StartCrazyness()
    {
        destroyed = true;
        timer?.StartCounting();
        StartCoroutine(CrowdFadeOut());
        StartCoroutine(ScreamAndMusic());
    }

    IEnumerator CrowdFadeOut()
    {
        if(crowd)
        {
            for(float i = 1; i > 0 ; i -= Time.deltaTime * 3f)
            {
                crowd.volume = Mathf.Clamp01(i);
                yield return null;
            }
        }
        crowd.Stop();
    }

    IEnumerator ScreamAndMusic()
    {
        if(scream && metal)
        {
            scream.Play();
            yield return new WaitForSeconds(scream.clip.length);
            animator?.Play(animationName);
            metal.Play();
            camNoise.AmplitudeGain = amplitude;
            camNoise.FrequencyGain = frequency;
        }
    }
}
