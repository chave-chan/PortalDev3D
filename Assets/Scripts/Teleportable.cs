using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    [SerializeField] private float teleportOffset;
    private bool isTeleporting = false;
    private Vector3 teleportPosition;
    private Vector3 teleportForward;
    public bool isActive = true;
    /*
     * When being near a portal, the player teleports to the position of the other portal
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PortalCamera portal) && isActive)
        {
            isTeleporting = true;
            Vector3 l_Position = portal.virtualPortal.InverseTransformPoint(transform.position);
            Vector3 l_Direction = portal.virtualPortal.InverseTransformDirection(transform.forward);
            teleportForward = portal.otherPortal.transform.TransformDirection(l_Direction);
            teleportPosition = portal.otherPortal.transform.TransformPoint(l_Position) + portal.otherPortal.transform.forward * teleportOffset;

            if(TryGetComponent(out Rigidbody rb))
            {
                Vector3 l_Velocity = portal.virtualPortal.transform.InverseTransformDirection(rb.velocity);
                rb.velocity = portal.otherPortal.transform.TransformDirection(l_Velocity);
                transform.localScale *= (portal.otherPortal.transform.localScale.x / portal.transform.localScale.x);
            }
        }
    }

    private void LateUpdate()
    {
        if (isTeleporting)
        {
            isTeleporting = false;
            if(TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = false;
            }
            transform.position = teleportPosition;
            transform.forward = teleportForward;
            if(TryGetComponent(out FPSController fpsController))
            {
                fpsController.recalculateYawPitch();
            }
            if(TryGetComponent(out CharacterController characterController1))
            {
                characterController1.enabled = true;
            }
        }
    }
}
