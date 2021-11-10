using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPortal : MonoBehaviour
{
    [SerializeField] private List<Transform> controlPoints;
    [SerializeField] private string portalEnabledTag;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxNormalAngle;
    [SerializeField] private float maxDistance;
    public bool isValidPosition(Camera mainCamera)
    {
        foreach (var point in controlPoints)
        {
            if (Physics.Raycast(mainCamera.transform.position, point.position - mainCamera.transform.position, out RaycastHit hitInfo, float.MaxValue, layerMask))
            {
                if (!hitInfo.transform.gameObject.CompareTag(portalEnabledTag))
                {
                    Debug.Log("Invalid Position: TAG");
                    return false;
                }
                if (Vector3.Angle(hitInfo.normal, point.forward) > maxNormalAngle)
                {
                    Debug.Log("Invalid Position: ANGLE");
                    return false;
                }
                if ((hitInfo.point - point.position).magnitude > maxDistance)
                {
                    Debug.Log("Invalid Position: DISTANCE");
                    return false;
                }
            }
            else
            {
                Debug.Log("Invalid Position: NO RAYCAST");
                return false;
            }
        }
        return true;
    }
}
