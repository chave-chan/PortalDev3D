using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] public PortalCamera otherPortal;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] public Transform virtualPortal;
    [SerializeField] private Transform portalCamera;

    [SerializeField] private float nearClipOffset;

    private void Update() 
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCameraTransform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

        Vector3 local_direction = virtualPortal.InverseTransformDirection(playerCameraTransform.forward);
        otherPortal.portalCamera.transform.forward = otherPortal.playerCameraTransform.TransformDirection(local_direction);

        float playerDist = (playerCameraTransform.position - transform.position).magnitude;
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = playerDist + nearClipOffset;
        
        otherPortal.portalCamera.GetComponent<Camera>().fieldOfView = 100 / (playerDist);
    }
}
