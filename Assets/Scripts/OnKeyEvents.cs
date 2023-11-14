using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct KeyEvent
{
    public KeyCode key;
    public UnityEvent EventKeyPressed;
    public UnityEvent EventKeyReleased;
}

public class OnKeyEvents : MonoBehaviour
{
    public List<KeyEvent> keyEvents = new List<KeyEvent>();
    
    void Update()
    {
        foreach (var kEvent in keyEvents)
        {
            if (Input.GetKeyDown(kEvent.key)) kEvent.EventKeyPressed?.Invoke();
            if (Input.GetKeyUp(kEvent.key)) kEvent.EventKeyReleased?.Invoke();
        }
    }
}
