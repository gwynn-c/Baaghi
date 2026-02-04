using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly Enemy controller;
    protected readonly Animator animator;
    
    
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int AwareHash = Animator.StringToHash("Aware");
    protected static readonly int ShootHash = Animator.StringToHash("Shoot");
    protected static readonly int ReloadHash = Animator.StringToHash("Reload");
    protected static readonly int DeathHash = Animator.StringToHash("Death");
    
    protected const float crossFadeDuration = 0.1f;

    protected BaseState(Enemy controller, Animator animator)
    {
        this.controller = controller;
        this.animator = animator;
    }
    public virtual void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
        throw new System.NotImplementedException();
    }
}