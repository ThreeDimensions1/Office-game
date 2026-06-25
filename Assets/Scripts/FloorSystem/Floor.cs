using UnityEngine;

[CreateAssetMenu(menuName = "Floors/Floor")]
public class Floor : ScriptableObject
{
    public string sceneName;
    public int floorID = 0;
    [Header("Floor info")]
    public int gameTime = 60;
    public string floorName;
    public Sprite image;
    [Header("Stars")]

    public int oneStar = 1000;
    public int twoStars = 2000;
    public int threeStars = 3000;
}
