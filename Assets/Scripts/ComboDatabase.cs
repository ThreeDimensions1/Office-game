using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboDatabase", menuName = "SO/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    public List<ComboTierData> comboTiers;

    public ComboTierData GetTierForCombo(int currentCombo) {
        if(currentCombo < comboTiers[0].comboRange.x) return comboTiers[0];

        for (int i = 0; i < comboTiers.Count; i++) {
            if (comboTiers[i].IsInTier(currentCombo))
            {
                return comboTiers[i];
            }
        }
        return comboTiers[comboTiers.Count - 1];
    }
}
[System.Serializable]
public class ComboTierData
{
    [Header("Combo Range (Min to Max)")]
    [Tooltip("X = Min Combo Hits, Y = Max Combo Hits for this tier")]
    public Vector2Int comboRange;

    [Header("UI Juice")]
    public string flavorText;
    public Color textColor = Color.white;

    [Header("Gameplay Modifiers")]
    public float scoreMultiplier = 1.0f;

    // Quick helper to check if a current combo fits in this tier
    public bool IsInTier(int currentCombo) {
        return currentCombo >= comboRange.x && currentCombo <= comboRange.y;
    }
}