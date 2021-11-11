using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameOver image;

    public void GameOver()
    {
        image.setGameOver();
        player.GetComponent<PortalGun>().enabled = false;
        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void Restart()
    {
        Debug.Log("RESTART");
        image.setGameRestarted();
        player.GetComponent<PortalGun>().enabled = true;
        player.GetComponent<FPSController>().enabled = true;
        player.GetComponent<FPSController>().Restart();
        player.GetComponent<CharacterController>().enabled = true;
    }
}
