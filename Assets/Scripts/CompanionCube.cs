using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : MonoBehaviour
{
    [SerializeField] private Transform initTransform;
    void Start()
    {
        transform.position = initTransform.position;
    }
}
