using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalGun : MonoBehaviour
{
    //Portal Preview
    [SerializeField] private string portalEnabledTag;
    [SerializeField] private GameObject previewPortal;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask portalLayerMask;
    [SerializeField] private Camera mainCamera;
    private bool isActive;
    //Blue Portal and Orange Portal
    [SerializeField] private GameObject bluePortal;
    [SerializeField] private GameObject orangePortal;
    //Gunsight
    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite bothSprite;

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActive = movePreviewPortal();
            if (isActive)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) { changeGunsight(false, false); }
                else if (Input.GetMouseButton(0)) { changeGunsight(false, orangePortal); }
                else if (Input.GetMouseButton(1)) { changeGunsight(bluePortal, false); }
            }
        }
        previewPortal.SetActive(isActive && isActive);
        if (Input.GetMouseButtonUp(0))
        {
            bluePortal.SetActive(true);
            bluePortal.transform.position = previewPortal.transform.position;
            bluePortal.transform.forward = previewPortal.transform.forward;
            changeGunsight(bluePortal.activeInHierarchy, orangePortal.activeInHierarchy);
        }
        if (Input.GetMouseButtonUp(1) && isActive)
        {
            orangePortal.SetActive(true);
            orangePortal.transform.position = previewPortal.transform.position;
            orangePortal.transform.forward = previewPortal.transform.forward;
            changeGunsight(bluePortal.activeInHierarchy, orangePortal.activeInHierarchy);
        }
    }
    /*
     * Shows a preview of a portal if the surface where the player is looking has the tag PortalEnabled
     */
    bool movePreviewPortal()
    {
        Ray r = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if (Physics.Raycast(r, out RaycastHit hitInfo, maxDistance, portalLayerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag(portalEnabledTag))
            {
                previewPortal.transform.position = hitInfo.point;
                previewPortal.transform.forward = hitInfo.normal;
                return previewPortal.GetComponent<PreviewPortal>().isValidPosition(mainCamera);
            }
        }
        return false;
    }

    void changeGunsight(bool bluePortal, bool orangePortal)
    {
        if (!bluePortal && !orangePortal) { image.sprite = noneSprite; }
        if (bluePortal && !orangePortal) { image.sprite = blueSprite; }
        if (!bluePortal && orangePortal) { image.sprite = orangeSprite; }
        if (bluePortal && orangePortal) { image.sprite = bothSprite; }
    }
}
