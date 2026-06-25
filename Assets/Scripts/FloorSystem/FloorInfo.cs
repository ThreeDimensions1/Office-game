using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorInfo : MonoBehaviour
{
    public static FloorInfo Instance;
    public Floor currentFloor; // {get; private set; }
    public string nextFloorSceneName;
    public string nextFloorLvlName;
    FloorList list;

    void Awake()
    {
        if(!Instance) Instance = this;
        else Destroy(this);
        string sceneName = SceneManager.GetActiveScene().name;
        list = Resources.Load<FloorList>("Floors");
        // foreach(Floor flr in list.floors)
        // {
        //     if(flr.sceneName == sceneName)
        //     {
        //         currentFloor = flr;
        //         break;
        //     }
        // }
        for(int i = 0; i < list.floors.Count; i++)
        {
            if(list.floors[i].sceneName == sceneName)
            {
                currentFloor = list.floors[i];
                if(!(i + 1 == list.floors.Count))
                {
                    nextFloorSceneName = list.floors[i + 1].sceneName;
                    nextFloorLvlName = list.floors[i + 1].floorName;
                }
                else
                {
                    nextFloorSceneName = "Main Menu";
                    nextFloorLvlName = "Exit to main menu.";
                }
                
                break;
            }
        }
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
