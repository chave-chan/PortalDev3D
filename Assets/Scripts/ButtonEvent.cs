using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private KeyCode buttonKeyCode;
    [SerializeField] private string buttonName;
    private bool isActive = true;
    [SerializeField] private bool onceTrigger;
    [SerializeField] private UnityEvent buttonEvent;

    public bool pressButton()
    {
        bool canBeCalled = isActive;
        if (isActive) { buttonEvent.Invoke(); }
        if (onceTrigger) { isActive = false; }
        return canBeCalled;
    }

    public KeyCode getKeyCode()
    {
        return buttonKeyCode;
    }

    public string getButtonName()
    {
        return buttonName;
    }
}
