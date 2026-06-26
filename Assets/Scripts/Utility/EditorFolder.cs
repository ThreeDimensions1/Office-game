using UnityEngine;

public class EditorFolder : MonoBehaviour
{
    void Awake() {
        if (transform.localScale != Vector3.one) {
            Debug.LogWarning($"[{name}] Root folder has a scale other than 1,1,1! " +
                             "Detaching physics objects may cause physics bugs. Bitch.", gameObject);
        }

        // Explicitly tells Unity: "Keep their world pos/rot exactly as they look right now"
        foreach (Transform child in transform) {
            child.SetParent(null, true); 
        }
        
        Destroy(gameObject);
    }
}
