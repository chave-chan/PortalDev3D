using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animation _Animation;
    private bool isOpen = false;
    //Open the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() && !isOpen)
        {
            _Animation.CrossFade("Door_Open", 0.005f);
            isOpen = true;
        }
    }
    //Close the door
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(coroutineWaitDoor(5.0f));
        if (other.gameObject.GetComponent<Player>() && isOpen)
        {
            _Animation.CrossFade("Door_Close", 0.005f);
            isOpen = false;
        }
    }
    IEnumerator coroutineWaitDoor(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
