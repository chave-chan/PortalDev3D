using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompanionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 multiSpawnOffset;
    public void spawn(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(objectToSpawn, transform.position + multiSpawnOffset * i, transform.rotation);
        }
    }
}
