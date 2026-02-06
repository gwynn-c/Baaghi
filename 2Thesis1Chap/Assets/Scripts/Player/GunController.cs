using StarterAssets;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private StarterAssetsInputs input;    
    public int maxAmmo = 30;
    public LayerMask enemiesLayerMask;
    public LayerMask playerLayerMask;
                                           
    public Transform gunBarrelTransform;
    public GameObject hitImpactVFX;
    
    public float fireRange, spread, timeBetweenShots, reloadTime;
    
    public int bulletsLeft, bulletsFired, bulletsPerShot, damage;
    public bool isShooting, isReloading, isReadyToShoot, allowInvoke;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsLeft = maxAmmo;
        isReadyToShoot = true;
        isReloading = false;
    }

 

    public void GunInputHandler(StarterAssetsInputs _input, Ray _ray)
    {
        input = _input;
        isShooting = _input.attack;
        if (_input.attack)
        {
            if(isReadyToShoot && isShooting && !isReloading && bulletsLeft <= 0) Reload();
        
            if ( isShooting && isReadyToShoot && !isReloading && bulletsLeft > 0)
            {
                bulletsFired = 0;
                Shoot(_ray);
                _input.attack = false;

            }
            if (_input.reload && bulletsLeft < maxAmmo && !isReloading)
            {
                Reload();
            }
        }
        
    }
    
    
    public void Shoot(Ray _ray)
    {
        isReadyToShoot = false;
        
        bulletsFired++;
        bulletsLeft--;
     
        var targetPosition = Physics.Raycast(_ray, out var hit, fireRange) ? hit.point : _ray.GetPoint(fireRange);
    
        var x = Random.Range(-spread, spread);
        var y = Random.Range(-spread, spread);
        var directionWithSpread = targetPosition + new Vector3(x, y, 0);
        Ray ray2 = new Ray(gunBarrelTransform.position, directionWithSpread.normalized);
        if (gameObject.CompareTag("Enemy"))
        {
            Physics.Raycast(_ray, out var hit2, fireRange, playerLayerMask);

        }
        else if (gameObject.CompareTag("Player"))
        {
            Physics.Raycast(_ray, out var hit2, fireRange, enemiesLayerMask);
        }
        if(hitImpactVFX != null)
            Instantiate(hitImpactVFX, targetPosition, Quaternion.identity);
        
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
            
        }
        
        if(bulletsFired < bulletsPerShot && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }


    void ResetShot()
    {
        isReadyToShoot = true;
        allowInvoke = true;
    }

    public void Reload()
    {
        isReloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = maxAmmo;
        isReloading = false;
    }

}
