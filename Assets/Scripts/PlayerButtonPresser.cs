using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ButtonEvent_1 = ButtonEvent;

public class PlayerButtonPresser : MonoBehaviour
{
    [SerializeField] private Door_Button door;
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
            door.turnLight1Off();
            door.turnLight2Off();
            if (door.light1)
            {
                door.turnLight2Off();
                door.turnLight1On();
            }
            if (door.light2)
            {
                door.turnLight1Off();
                door.turnLight2On();
            }
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
