using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Floors/Floor List")]
public class FloorList : ScriptableObject
{
    public List<Floor> floors = new();
}
