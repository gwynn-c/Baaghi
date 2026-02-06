using UnityEngine;
using UnityEngine.AI;

public class AwareState : EnemyBaseState
{
    readonly NavMeshAgent agent;
    readonly float detectionTime;
    public AwareState(Enemy controller, Animator animator, NavMeshAgent agent, float detectionTime) : base(controller, animator)
    {
        this.detectionTime = detectionTime;
        this.agent = agent;
    }
    private float currentDetectionTime = 0f;
    public override void OnEnter()
    {
        Debug.Log("Enemy is Looking for you");
        agent.isStopped = true;
        animator.CrossFade(AwareHash, crossFadeDuration);
        
    }
    public override void Update()
    {
        if (controller.playerInFOV)
        {
            if(currentDetectionTime < detectionTime)
                currentDetectionTime += Time.deltaTime;
            else 
                controller.playerDetected = true;
        }
    }

    public override void OnExit()
    {
        currentDetectionTime = 0f;
        agent.isStopped = false;
        
    }
}