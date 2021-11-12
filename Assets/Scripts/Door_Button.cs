using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Button : MonoBehaviour
{
    [SerializeField] private Animation animation;
    [SerializeField] private MeshRenderer light1Off, light2Off, light1On, light2On;
    public bool light1 = false, light2 = false;
    private bool isOpen = false;

    private void Start()
    {
        turnLight1Off();
        turnLight2Off();
    }

    private void Update()
    {
        openDoor();
        closeDoor();
    }
    //Open the door
    private void openDoor()
    {
        if (light1 && light2 && !isOpen)
        {
            animation.CrossFade("Door_Open", 0.005f);
            isOpen = true;
        }
    }
    //Close the door
    private void closeDoor()
    {
        if ((!light1 || !light2) && isOpen)
        {
            animation.CrossFade("Door_Close", 0.005f);
            isOpen = false;
        }
    }

    public void turnLight1On()
    {
        light1 = true;
        light1Off.enabled = false;
        light1On.enabled = true;
    }

    public void turnLight2On()
    {
        light2 = true;
        light2Off.enabled = false;
        light2On.enabled = true;
    }

    public void turnLight1Off()
    {
        light1 = false;
        light1Off.enabled = true;
        light1On.enabled = false;
    }

    public void turnLight2Off()
    {
        light2 = false;
        light2Off.enabled = true;
        light2On.enabled = false;
    }

    IEnumerator coroutineWaitDoor(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
