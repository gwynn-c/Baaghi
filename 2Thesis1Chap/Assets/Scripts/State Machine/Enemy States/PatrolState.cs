using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyBaseState
{
    readonly NavMeshAgent agent;
    readonly Vector3 startPosition;
    readonly float patrolRadius;
    
    
    public PatrolState(Enemy controller, Animator animator, NavMeshAgent agent, float patrolRadius ) : base(controller, animator)
    {
        this.agent = agent;
        this.startPosition = controller.transform.position;
        this.patrolRadius = patrolRadius;
        
    }

    public override void OnEnter()
    {
        Debug.Log("Patrol State");
        animator.CrossFade(LocomotionHash, crossFadeDuration);
    }

    public override void Update()
    {

        if (HasReachedDestination())
        {
            var randomDirection = Random.insideUnitSphere * patrolRadius;

            randomDirection += startPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
            
            var finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    public override void OnExit()
    {
        
    }
    private bool HasReachedDestination()
    {
        
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }
    
}