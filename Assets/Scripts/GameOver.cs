using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image image;

    void Start()
    {
        if (image != null)
            image.enabled = false;
    }

    public void setGameOver()
    {
        if (image != null)
            image.enabled= true;
    }

    public void setGameRestarted()
    {
        if (image != null)
            image.enabled = false;
    }
}
