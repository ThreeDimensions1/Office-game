using System;
using UnityEngine;
using UnityEngine.Pool;

public class Ui_ScoreBoard : MonoBehaviour 
{
    public Transform parent;
    public UiScoreCounter ScorePopupPrefab;
    private ObjectPool<UiScoreCounter> _pool;

    void Awake() {
        // Initialize the pool with its rules
        _pool = new ObjectPool<UiScoreCounter>(
            createFunc: CreatePooledItem,       // How to make a new one if pool is empty
            actionOnGet: OnTakeFromPool,        // What to do when waking it up
            actionOnRelease: OnReturnedToPool,  // What to do when disabling it
            actionOnDestroy: OnDestroyPoolObject, // Cleanup if pool gets too big
            collectionCheck: true,              // Throws errors if you release the same object twice
            defaultCapacity: 20,                // Pre-allocates memory for 20 items
            maxSize: 100                        // Absolute limit of items alive in memory
        );
    }
    private UiScoreCounter CreatePooledItem() {
        UiScoreCounter go = Instantiate(ScorePopupPrefab, parent);
        return go;
    }
    private void OnTakeFromPool(UiScoreCounter go) {
        go.gameObject.SetActive(true);
    }
    private void OnReturnedToPool(UiScoreCounter go) {
        go.gameObject.SetActive(false);
    }
    private void OnDestroyPoolObject(UiScoreCounter go) {
        Destroy(go);
    }

    void Update() {
        
    }
    void OnEnable() {
        ScoreManager.Instance.ScoreUpdate += onScoreUpdate;
        ScoreManager.Instance.ComboUpdate += onComboUpdate;
    }
    private void onScoreUpdate(string obj) {
        
    }
    private void onComboUpdate(int obj) {
        
    }

    
}