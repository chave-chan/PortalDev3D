using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionButtonPresser : MonoBehaviour
{
    private List<ButtonEvent> activeButtons = new List<ButtonEvent>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            Debug.Log("Activate" + button.getButtonName() + " using a Companion Cube.");
            button.pressButton();
        }
    }
}
