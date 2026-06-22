using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Action<string> ScoreUpdate;
    public Action<int, float, string> ComboUpdate;

    void Awake() {
        if(Instance) {
            Debug.LogError("ScoreManager singleton already exists!");
            Destroy(gameObject);
        }
        Instance = this;
    }

    [Header("Values")]
    [SerializeField] private int score;
    public int Score => score;
    [SerializeField] private int combo;
    public int Combo => combo;
    [SerializeField] private float comboClock = 0f;
    public float ComboClock => comboClock;
    [SerializeField] private float comboClockMax = 5f;
    public float ComboClockMax => comboClockMax;

    [Header("Score gain:")]
    public int[] scorePerTier = new int[3];

    private float multiplier;

    [Header("Looks")]
    public ComboDatabase comboDatabase;
    [SerializeField] private string popupFormatting = "{1} - {0}pt";

    /*void OnEnable() {
        PlayerDestruction.Instance.onHit += ProcessColosion;
    }*/
    void Update() {
        if(combo > 0) {
            comboClock -= Time.deltaTime;
            if(comboClock <= 0) {
                combo = 0;
                UpdateCombo();
            }
        }
    }
    void IncreaseCombo(int value) {
        combo += value;
        comboClock = comboClockMax;
        UpdateCombo();
    }
    void UpdateCombo() {
        ComboTierData currentTier = comboDatabase.GetTierForCombo(combo);
        multiplier = currentTier.scoreMultiplier;

        ComboUpdate.Invoke(combo, multiplier, currentTier.flavorText);
    }
    // public float GetMultiplier() {
    //     return 1f;
    // }
    // extremely lean, complexity later
    public void RegisterDestruction(ScoreProfile scoreProfile, string name) {
        IncreaseCombo(1);
        int scoreGain = scorePerTier[(int)scoreProfile];

        scoreGain = (int)(scoreGain * multiplier);
        string scoreLabel = string.Format(popupFormatting, scoreGain, name);
        ScoreUpdate?.Invoke(scoreLabel);

        score += scoreGain;
    }
    /*private void ProcessColosion(DestructionObject @object, float arg2) {
        if(@object.destroyVelocity <= arg2) {
            
        }
        // scoreUpdate?.Invoke();
    }*/
}

[Serializable]
public enum ScoreProfile {
    Small, 
    Medium, 
    Large
}