using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private float maxLaserDist;
    [SerializeField] private LayerMask laserLayerMask;
    private bool isActive = true;

    public void updateState(bool isActive)
    {
        laserRenderer.enabled = isActive;
        this.isActive = isActive;
    }

    private void Update()
    {
        if (isActive)
        {
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, maxLaserDist, laserLayerMask))
            {
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);
                //Laser refraction
                if(hitInfo.transform.gameObject.TryGetComponent(out TurretLaser laser))
                {
                    laser.updateState(true);
                }
                /*
                if (hitInfo.transform.gameObject.TryGetComponent(out HealthSystem hs))
                {
                    hs.kill();
                }*/
            }
            else
            {
                laserRenderer.SetPosition(1, Vector3.forward * maxLaserDist);
            }
        }    
    }
}
