using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class Ui_ScoreBoard : MonoBehaviour 
{
    public static Ui_ScoreBoard Instance;
    [Header("Popups:")]
    public Transform parent;
    public UiScoreCounter ScorePopupPrefab;
    public float CounterLifetime;
    [SerializeField] private string popupFormatting = "{1} - {0}pt";
    [SerializeField] private string scoreFormatting = "{0}$";
    private ObjectPool<UiScoreCounter> _pool;
    private List<UiScoreCounter> activeCounters = new();

    [Header("Combo:")]
    public TMP_Text textScore;
    public TMP_Text textCombo;
    public TMP_Text textMultiplier;
    public TMP_Text textFlavor;

    public int score {get; private set; }


    void Awake() {
        // Initialize the pool with its rules
        _pool = new ObjectPool<UiScoreCounter>(
            createFunc: CreatePooledItem,       // How to make a new one if pool is empty
            actionOnGet: OnTakeFromPool,        // What to do when waking it up
            actionOnRelease: OnReturnedToPool,  // What to do when disabling it
            actionOnDestroy: OnDestroyPoolObject, // Cleanup if pool gets too big
            collectionCheck: true,              // Throws errors if you release the same object twice
            defaultCapacity: 20,                // Pre-allocates memory for 20 items
            maxSize: 50                        // Absolute limit of items alive in memory
        );
        textScore.text = string.Format(scoreFormatting, 0);
        Instance = this;
    }
    void OnDestroy()
    {
        Instance = null;
    }
    private UiScoreCounter CreatePooledItem() {
        UiScoreCounter go = Instantiate(ScorePopupPrefab, parent);
        return go;
    }
    private void OnTakeFromPool(UiScoreCounter go) {
        go.gameObject.SetActive(true);
        activeCounters.Add(go);
    }
    private void OnReturnedToPool(UiScoreCounter go) {
        go.gameObject.SetActive(false);
        activeCounters.Remove(go);
    }
    private void OnDestroyPoolObject(UiScoreCounter go) {
        Destroy(go.gameObject);
    }

    void Update() {
        for (int i = activeCounters.Count - 1; i >= 0; i--) {
            activeCounters[i].lifetime -= Time.deltaTime;
            if(activeCounters[i].lifetime < 0) _pool.Release(activeCounters[i]);
        }
    }
    void OnEnable() {
        ScoreManager.Instance.ScoreUpdate += onScoreUpdate;
        ScoreManager.Instance.ComboUpdate += onComboUpdate;
    }
    private void onScoreUpdate(int scoreGain, int score) {
        if (activeCounters.Count >= 40) {
            activeCounters[0].lifetime = -1f;
        }
    
        string scoreLabel = string.Format(popupFormatting, scoreGain, name);

        UiScoreCounter counter = _pool.Get();
        counter.transform.SetAsFirstSibling();
        counter.UpdateContent(scoreLabel);
        counter.lifetime = CounterLifetime;

        textScore.text = string.Format(scoreFormatting, score);
        this.score = score;
    }
    private void onComboUpdate(int combo, float multiplier, string flavorText) {
        textCombo.text = string.Format("{0}x", combo);
        textMultiplier.text = string.Format("{0}x", multiplier.ToString("F1"));
        textFlavor.text = flavorText;
    }

    public void SaveProgress()
    {
        Progress.SaveProgress($"Floor{FloorInfo.Instance.currentFloor.floorID}", score);
    }
}