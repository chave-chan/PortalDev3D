using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform initTransform;
    [SerializeField] private KeyCode restartKey;
    [SerializeField] private int health = 100;


    private int currentHealth = 0;
    private bool dead = false;

    private void Start()
    {
        currentHealth = health;
        transform.position = gameManager.lastCheckPointPos;

    }
    private void Update()
    {
        if (dead && Input.GetKeyDown(restartKey))
        {
            restart();
        }
    }


    public void die()
    {
        dead = true;
        health = 0;
        gameManager.GameOver();
    }

    public void restart()
    {
        gameManager.Restart();
        currentHealth = health;
    }

}
