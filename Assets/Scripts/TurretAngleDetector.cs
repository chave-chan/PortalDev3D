using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretAngleDetector : MonoBehaviour
{
    [SerializeField] private float maxAngle;
    private bool isActive = true;
    [SerializeField] private UnityEvent activate;
    [SerializeField] private UnityEvent deactivate;

    private void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if(isActive && angle > maxAngle)
        {
            isActive = false;
            deactivate.Invoke();
        }
        if(!isActive & angle < maxAngle)
        {
            isActive = true;
            activate.Invoke();
        }
    }
}
