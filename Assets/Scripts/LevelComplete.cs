using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        if (text != null)
            text.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            setLevelComplete();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (text != null)
                text.enabled = false;
        }
    }

    public void setLevelComplete()
    {
        if (text != null)
            text.enabled = true;
    }
}
