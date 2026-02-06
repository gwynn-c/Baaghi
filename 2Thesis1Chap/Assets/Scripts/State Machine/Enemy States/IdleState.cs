using UnityEngine;

public class IdleState : EnemyBaseState
{
    public IdleState(Enemy controller, Animator animator) : base(controller, animator)
    {
        
        
    }

    public override void OnEnter()
    {
        animator.CrossFade(IdleHash, crossFadeDuration);
    }

    public override void Update()
    {
        
    }
}