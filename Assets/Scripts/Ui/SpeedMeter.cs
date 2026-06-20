using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    public float maxMagnitude = 25;
    public float magnitudeMultiplier = 1;
    public TMP_Text speedText;

    [Header("Image")]
    public Image image;
    public Color colorSlow = Color.green;
    public Color colorFast = Color.orange;

    Rigidbody playerRb;

    float magnitude;

    void Start()
    {
        playerRb = PlayerMovement.Instance.rb;
    }

    void FixedUpdate()
    {
        float currentMagnitude = playerRb.linearVelocity.magnitude;
        if(magnitude != currentMagnitude)
        {
            speedText.text = $"{Mathf.RoundToInt(currentMagnitude * magnitudeMultiplier)} km/h";
        }
        magnitude = currentMagnitude;
        
        float fillTarget = currentMagnitude / (maxMagnitude * magnitudeMultiplier);
        if (!Mathf.Approximately(fillTarget, image.fillAmount))
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, fillTarget, Time.deltaTime * 1);
            image.color = Color.Lerp(colorSlow, colorFast, image.fillAmount);
        }
    }
}
