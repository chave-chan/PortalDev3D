using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ButtonEvent_1 = ButtonEvent;

public class PlayerButtonPresser : MonoBehaviour
{
    [SerializeField] private KeyCode buttonKey;
    private List<ButtonEvent> activeButtons = new List<ButtonEvent>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            activeButtons.Add(button);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            activeButtons.Remove(button);
        }
    }

    private void Update()
    {
        foreach (var button in activeButtons)
        {
            Debug.Log("Activate" + button.getButtonName() + " pressing: " + button.getKeyCode());
            if (Input.GetKeyDown(button.getKeyCode()))
            {
                button.pressButton();
            }
        }
    }
}
