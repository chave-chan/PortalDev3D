using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode restartKey;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform initTransform;
    [SerializeField] private GameOver image;

    public void GameOver()
    {
        Restart();
    }

    public void Restart()
    {
        if (Input.GetKeyDown(restartKey))
        {
            image.setGameRestarted();
            player.SetActive(true);
            player.GetComponent<Player>().restart();
            player.transform.position = initTransform.position;
            player.transform.rotation = initTransform.rotation;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    public void GameOverScreen()
    {
        image.setGameOver();
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PortalGun>().enabled = false;
    }
}
