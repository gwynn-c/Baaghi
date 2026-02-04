using UnityEngine;
using UnityEngine.AI;

public class EngagedState : BaseState
{
    readonly NavMeshAgent agent;
    readonly Transform player;
    public EngagedState(Enemy controller, Animator animator, NavMeshAgent agent, Transform player) : base(controller, animator)
    {
        this.agent = agent;
        this.player = player;
        
    }

    public override void OnEnter()
    {
        Debug.Log("Chasing");
        animator.CrossFade(LocomotionHash, crossFadeDuration);
        
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
    }
}