using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OpenPause : MonoBehaviour
{
    [SerializeField] UnityEvent onEsc;
    void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            onEsc.Invoke();
        }
    }
}
