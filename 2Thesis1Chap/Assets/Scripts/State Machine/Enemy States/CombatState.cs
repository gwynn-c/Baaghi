using UnityEngine;

public class CombatState : EnemyBaseState
{
    protected readonly Transform player;
    protected readonly GunController gun;
    protected readonly float attackDistance;

    
    public CombatState(Enemy controller, Animator animator, Transform player,GunController gun, float attackDistance) : base(controller, animator)
    {
        this.player = player;
        this.attackDistance = attackDistance;
        this.gun = gun;
    }

    public override void OnEnter()
    {
        //Check for cover?
        Debug.Log("Engaging Player in combat");
        //Check if player is in Line of sight?
        //Prioritize whether to take cover or shoot? 
       
    }

    public override void Update()
    {
        controller.transform.LookAt(player.position);
        animator.SetBool(ReloadHash, gun.isReloading);

        if(gun.isReadyToShoot  && !gun.isReloading && gun.bulletsLeft <= 0) Reload();
        
        if (gun.isReadyToShoot && !gun.isReloading && gun.bulletsLeft > 0)
        {
            gun.bulletsFired = 0;
            Shoot();

        }
        if (gun.bulletsLeft < gun.maxAmmo && gun.isReloading)
        {
            Reload();
        }
    }

    public override void OnExit()
    {
        
    }
    
    void Shoot()
    {
        animator.CrossFade(ShootHash, crossFadeDuration);
        var ray = new Ray(gun.gunBarrelTransform.position, controller.transform.forward);
        gun.Shoot(ray);
    }
    void Reload()
    {
        gun.Reload();
    }
    
}