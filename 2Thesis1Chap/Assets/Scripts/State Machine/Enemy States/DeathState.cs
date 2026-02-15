using UnityEngine;

public class DeathState : EnemyBaseState
{
    public DeathState(Enemy controller, Animator animator) : base(controller, animator)
    {
        
    }


    public override void OnEnter()
    {
        animator.SetBool(DeathHash, true);
        
        
        //after disable agent, disable everything
    }
}