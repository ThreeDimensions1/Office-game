using TMPro;
using UnityEngine;

[RequireComponent(typeof(PaperButton))]
public class FloorButton : MonoBehaviour
{
    public Floor flr;
    public FloorUi ui;
    PaperButton button;
    void Awake()
    {
        button = GetComponent<PaperButton>();
        FloorList list = Resources.Load<FloorList>("Floors");
        GetComponentInChildren<TMP_Text>().text =
@$"Floor {flr.floorID + 1} -
{flr.floorName}";
        button.onClick.AddListener(OnClick);
        for(int i = 0; i < list.floors.Count; i++)
        {
            if(list.floors[i] == flr && i > 0)
            {
                button.active = Progress.LoadProgress($"Floor{i - 1}") >= list.floors[i - 1].oneStar;
                break;
            }
        }
    }

    void OnClick()
    {
        ui.SetUi(flr);
    }
}
