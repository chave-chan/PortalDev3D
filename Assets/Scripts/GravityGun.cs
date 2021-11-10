using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{

    Rigidbody takenObject;
    enum Status { taking, taken }
    Status currentStatus;

    [SerializeField] Transform attachPosition;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField] float moveSpeed;
    [SerializeField] float throwForce;
    [SerializeField] Camera playerCamera; 

    void Update()
    {
        if(Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("GravityShoot");
            takenObject = gravityShoot();
        }
        if (takenObject != null)
        {
            if (Input.GetMouseButton(2) && takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        updateTaking();
                        break;
                    case Status.taken:
                        updateTaken();
                        break;
                }
                if (Input.GetKeyDown(KeyCode.T)) { detachObject(throwForce); }
            }
            else
            {
                detachObject(0);
            }
        }
    }

    private Rigidbody gravityShoot()
    {
        if(Physics.Raycast(playerCamera.ViewportPointToRay(new Vector3 (0.5f, 0.5f)), out RaycastHit hit, 200.0f))
        {
            Rigidbody rb = hit.rigidbody;
            if(rb == null) { return null; }
            rb.isKinematic = true;
            if(rb.gameObject.TryGetComponent(out Teleportable tp))
            {
                tp.isActive = false;
            }
            initialPosition = rb.transform.position;
            initialRotation = rb.transform.rotation;
            currentStatus = Status.taking;
            return rb;
        }
        return null;
    }

    private void updateTaking()
    {
        takenObject.MovePosition(takenObject.position + (attachPosition.position - takenObject.position).normalized * moveSpeed * Time.deltaTime);
        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation, (takenObject.position - initialPosition).magnitude / (attachPosition.position - initialPosition).magnitude);
        if((attachPosition.position - takenObject.position).magnitude < 0.1f) { currentStatus = Status.taken; }
    }

    private void updateTaken()
    {
        takenObject.transform.position = attachPosition.position;
        takenObject.transform.rotation = attachPosition.rotation;
    }

    private void detachObject(float force)
    {
        if(takenObject.gameObject.TryGetComponent(out Teleportable tp))
        {
            tp.isActive = true;
        }
        takenObject.transform.parent = null;
        takenObject.isKinematic = false;
        takenObject.AddForce(attachPosition.forward * force);
        takenObject = null;
    }
}
