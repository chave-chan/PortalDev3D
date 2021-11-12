using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionButtonPresser : MonoBehaviour
{
    [SerializeField] private Door_Button door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            Debug.Log("Activate" + button.getButtonName() + " using a Companion Cube.");
            button.pressButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
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
}
