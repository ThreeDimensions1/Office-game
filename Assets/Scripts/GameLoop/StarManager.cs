using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class StarManager : MonoBehaviour
{
    public Image star1, star2, star3;
    public TMP_Text star1txt, star2txt, star3txt;
    public GameObject buttonRestart, buttonContinue;
    Floor floor;

    void Start()
    {
        floor = FloorInfo.Instance.currentFloor;
        star1txt.text = floor.oneStar.ToString() + "$";
        star2txt.text = floor.twoStars.ToString() + "$";
        star3txt.text = floor.threeStars.ToString() + "$";
    }

    public void StartAnim()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(1f);
        Ui_ScoreBoard board = Ui_ScoreBoard.Instance;
        if(board.score > floor.oneStar)
        {
            star1.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        if(board.score > floor.twoStars)
        {
            star2.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        if(board.score > floor.threeStars)
        {
            star3.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        if(board.score > floor.oneStar) buttonContinue.SetActive(true);
        buttonRestart.SetActive(true);
    }
}
