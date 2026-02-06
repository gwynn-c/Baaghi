using UnityEngine;

public abstract class EnemyBaseState : IState
{
    protected readonly Enemy controller;
    protected readonly Animator animator;
    
    
    protected static readonly int LocomotionHash = Animator.StringToHash("Patrol");
    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int ChaseHash = Animator.StringToHash("Run");
    protected static readonly int AwareHash = Animator.StringToHash("Aware");
    protected static readonly int ShootHash = Animator.StringToHash("Shoot");
    protected static readonly int ReloadHash = Animator.StringToHash("Reload");
    protected static readonly int DeathHash = Animator.StringToHash("Death");
    
    protected const float crossFadeDuration = 0.1f;

    protected EnemyBaseState(Enemy controller, Animator animator)
    {
        this.controller = controller;
        this.animator = animator;
    }
    public virtual void OnEnter()
    {
        //NOOP
    }

    public virtual void Update()
    {
        //NOOP
    }

    public virtual void FixedUpdate()
    {
        //NOOP
    }

    public virtual void OnExit()
    {
        //NOOP
    }
}