using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameOver image;
    [SerializeField] Vector3 initPlayerPos;
    public Vector3 lastCheckPointPos;

    private static GameManager instance;


    private void Start()
    {
        lastCheckPointPos = initPlayerPos;
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        image.setGameOver();
        player.GetComponent<PortalGun>().enabled = false;
        player.GetComponent<FPSController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void Restart()
    {
        image.setGameRestarted();
        if(lastCheckPointPos == initPlayerPos)
            SceneManager.LoadScene("SampleScene");
        else
            player.transform.position = lastCheckPointPos;

        player.GetComponent<PortalGun>().enabled = true;
        player.GetComponent<FPSController>().enabled = true;
        player.GetComponent<FPSController>().Restart();
        player.GetComponent<CharacterController>().enabled = true;
    }
}
