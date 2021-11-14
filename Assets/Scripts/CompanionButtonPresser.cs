using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionButtonPresser : MonoBehaviour
{
    [SerializeField] private Door_Button door;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Companion"))
        {
            gm.lastCheckPointPos = transform.position;
            gm.lastCheckPointPos += new Vector3(1, 0, 1);

            if (gameObject.CompareTag("Button1"))
            {
                door.turnLight1On();
            }
            if (gameObject.CompareTag("Button2"))
            {
                door.turnLight2On();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Button1"))
        {
            door.turnLight1Off();
        }
        if (gameObject.CompareTag("Button2"))
        {
            door.turnLight2Off();
        }
    }
}
