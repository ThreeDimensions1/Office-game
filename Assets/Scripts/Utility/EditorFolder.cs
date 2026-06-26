using UnityEngine;

public class EditorFolder : MonoBehaviour
{
    void Awake() {
        if (transform.localScale != Vector3.one) {
            Debug.LogWarning($"[{name}] Root folder has a scale other than 1,1,1! " +
                             "Detaching physics objects may cause physics bugs. Bitch.", gameObject);
        }

        for (int i = transform.childCount - 1; i >= 0; i--) {
            transform.GetChild(i).SetParent(null, true);
        }
        
        // Destroy(gameObject);
    }
}
