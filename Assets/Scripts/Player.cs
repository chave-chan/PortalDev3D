using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int health = 100;
    private int currentHealth = 0;

    private void Start()
    {
        currentHealth = health;
    }

    public void die()
    {
        health = 0;
        gameManager.GameOverScreen();
        Debug.Log("Player dead");
    }

    public void restart()
    {
        currentHealth = health;
    }
}
