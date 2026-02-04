using StarterAssets;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    
    public int maxAmmo = 30;
    public LayerMask enemiesLayerMask;
    public TextMeshProUGUI ammoText;

    public Transform gunBarrelTransform;
    public GameObject hitImpactVFX;
    
    public float fireRange, fireRate, spread, timeBetweenShots, reloadTime;

    public int bulletsLeft, bulletsFired, bulletsPerShot;
    public bool isShooting, isReloading, isReadyToShoot, allowInvoke;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsLeft = maxAmmo;
        isReadyToShoot = true;
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = bulletsLeft.ToString();
        //Move this into a player controller
        // if (_input.attack)
        // {
        //     isShooting = _input.attack;
        //     if(isReadyToShoot && isShooting && !isReloading && bulletsLeft <= 0) Reload();
        //
        //     if (isShooting && isReadyToShoot && !isReloading && bulletsLeft > 0)
        //     {
        //         bulletsFired = 0;
        //         Shoot();
        //     }
        //     if (_input.reload && bulletsLeft < maxAmmo && !isReloading)
        //     {
        //         Reload();
        //     }
        // }
    }


    public void Shoot()
    {
        isReadyToShoot = false;
        
        // instantiate muzzle flash
        bulletsFired++;
        bulletsLeft--;
        //Put in to playercontroller 
        // if player contrller
        // Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // var targetPosition = Physics.Raycast(ray, out var hit, fireRange) ? hit.point : ray.GetPoint(fireRange);
    
        var x = Random.Range(-spread, spread);
        var y = Random.Range(-spread, spread);
        // var directionWithSpread = targetPosition + new Vector3(x, y, 0);
        // Ray ray2 = new Ray(gunBarrelTransform.position, directionWithSpread.normalized);
        // Physics.Raycast(ray, out var hit2, fireRange, enemiesLayerMask);
        
        // Instantiate(hitImpactVFX, targetPosition, Quaternion.identity);
        
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
        // _input.attack = false;
    }

    private void Reload()
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
