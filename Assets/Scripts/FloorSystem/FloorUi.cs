using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorUi : MonoBehaviour
{
    public TMP_Text header;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text starText;
    public Sprite image;
    public SpriteRenderer imageRenderer;

    string scene;

    public void SetUi(Floor floor)
    {
        header.text = $"Floor {floor.floorID + 1} - {floor.floorName}";

        int minutes = floor.gameTime / 60;
        int seconds = floor.gameTime % 60;

        if (seconds == 0) timeText.text = $"Time: {minutes}:{seconds}0";
        else if (seconds < 10) timeText.text = $"Time: {minutes}:0{seconds}";
        else timeText.text = $"Time: {minutes}:{seconds}";
        if(seconds == 0) timeText.text += "0";

        scoreText.text = $"Best score: {Progress.LoadProgress($"Floor{floor.floorID}")}";
        Debug.Log(SaveSystem.Load($"Floor{floor.floorID}"));

        starText.text =
@$"1 Star: {floor.oneStar}
2 Stars: {floor.twoStars}
3 Stars: {floor.threeStars}";

        imageRenderer.sprite = image;

        scene = floor.sceneName;
    }
    public void OpenScene()
    {
        SceneManager.LoadScene(scene);
    }
}
