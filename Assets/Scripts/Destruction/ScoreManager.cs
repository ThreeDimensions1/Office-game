using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour 
{
    public static ScoreManager Instance;

    public Action<string> ScoreUpdate;
    public Action<int> ComboUpdate;

    void Awake() {
        if(Instance) {
            Debug.LogError("ScoreManager singleton already exists!");
            Destroy(gameObject);
        }
        Instance = this;
    }

    [SerializeField] private int score;
    public int Score => score;
    [SerializeField] private int combo;
    public int Combo => combo;
    [SerializeField] private float comboClock = 0f;
    public float ComboClock => comboClock;
    [SerializeField] private float comboClockMax = 5f;
    public float ComboClockMax => comboClockMax;

    /*void OnEnable() {
        PlayerDestruction.Instance.onHit += ProcessColosion;
    }*/
    void Update() {
        if(combo > 0) {
            comboClock -= Time.deltaTime;
            if(comboClock <= 0) {
                combo = 0;
            }
        }
    }
    void IncreaseCombo(int value) {
        combo += value;
        comboClock = comboClockMax;

        ComboUpdate.Invoke(combo); // unused for now
    }
    public float GetMultiplier() {
        return 1f;
    }
    // extremely lean, complexity later
    public void RegisterDestruction(int scoreGain, string name) {
        IncreaseCombo(1);

        scoreGain = (int)(scoreGain * GetMultiplier());
        string scoreLabel = string.Format("+{0}pt - {1}", scoreGain, name);
        ScoreUpdate?.Invoke(scoreLabel);

        score += scoreGain;
    }
    /*private void ProcessColosion(DestructionObject @object, float arg2) {
        if(@object.destroyVelocity <= arg2) {
            
        }
        // scoreUpdate?.Invoke();
    }*/
}