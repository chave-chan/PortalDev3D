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
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector3 minScale;

    private bool lastModBlue = false;
    private bool lastModOrange = false;
    private bool firstPortal = true;
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
        if(lastModBlue)
            bluePortal.transform.localScale = previewPortal.transform.localScale;

        if(lastModOrange)
            orangePortal.transform.localScale = previewPortal.transform.localScale;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActive = movePreviewPortal();
            if (isActive)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f && previewPortal.transform.localScale.magnitude < maxScale.magnitude) // to be bigger
                {
                    previewPortal.transform.localScale += new Vector3(0.25f, 0.25f, 0f);
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f && previewPortal.transform.localScale.magnitude > minScale.magnitude) // to be smaller
                {
                    previewPortal.transform.localScale -= new Vector3(0.25f, 0.25f, 0f);
                }

                if (Input.GetMouseButtonDown(0)) {
                    if (firstPortal)
                    {
                        changeGunsight(false, false);
                        firstPortal = false;
                    }
                    else
                        changeGunsight(false, orangePortal);

                    lastModBlue = true;
                    lastModOrange = false;
                }
                else if (Input.GetMouseButtonDown(1)) {
                    if (firstPortal)
                    {
                        changeGunsight(false, false);
                        firstPortal = false;
                    }
                    else
                        changeGunsight(bluePortal, false);

                    lastModBlue = false;
                    lastModOrange = true;
                }
            }
        }
        previewPortal.SetActive(isActive && isActive);
        if (Input.GetMouseButtonUp(0))
        {
            bluePortal.SetActive(true);
            bluePortal.transform.position = previewPortal.transform.position;
            bluePortal.transform.forward = previewPortal.transform.forward;
            changeGunsight(bluePortal.activeInHierarchy, orangePortal.activeInHierarchy);
            isActive = false;
        }
        if (Input.GetMouseButtonUp(1) && isActive)
        {
            orangePortal.SetActive(true);
            orangePortal.transform.position = previewPortal.transform.position;
            orangePortal.transform.forward = previewPortal.transform.forward;
            changeGunsight(bluePortal.activeInHierarchy, orangePortal.activeInHierarchy);
            isActive = false;
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
