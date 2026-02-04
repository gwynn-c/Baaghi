using UnityEngine;

public class CombatState : BaseState
{
    private readonly Transform player;
    private readonly GunController gun;
    
    public CombatState(Enemy controller, Animator animator, GunController gun, Transform player) : base(controller, animator)
    {
        this.player = player;
        this.gun = gun;
    }


    public override void OnEnter()
    {
        Debug.Log("Entering Combat");
    }

    public override void Update()
    {
        gun.Shoot();
    }
}