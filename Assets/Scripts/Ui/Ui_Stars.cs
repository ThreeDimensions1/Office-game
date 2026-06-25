using TMPro;
using UnityEngine;

public class Ui_Stars : MonoBehaviour
{
    TMP_Text text;

    int star1;
    int star2;
    int star3;

    int score;
    
    void Start()
    {
        Floor flr = FloorInfo.Instance.currentFloor;
        text = GetComponent<TMP_Text>();
        star1 = flr.oneStar;
        star2 = flr.twoStars;
        star3 = flr.threeStars;
        UpdateUI();
        Ui_ScoreBoard.Instance.onScoreChange += UpdateScore;
    }

    void UpdateScore(int i)
    {
        score = i;
        UpdateUI();
    }


    void UpdateUI()
    {
        text.text =
@$"<color={GetColor(star3)}> $ {star3} </color>
<color={GetColor(star2)}> $ {star2} </color>
<color={GetColor(star1)}> $ {star1} </color>";
    }

    string GetColor(int i)
    {
        if(score >= i) return "green";
        else return "white";
    }
}
