using UnityEngine;
using UnityEngine.AI;

public class AwareState : EnemyBaseState
{
    readonly NavMeshAgent agent;
    readonly float detectionTime;
    readonly AudioClip[] hmmSounds;
    public AwareState(Enemy controller, Animator animator, NavMeshAgent agent, float detectionTime, AudioClip[] awareSounds) : base(controller, animator)
    {
        this.detectionTime = detectionTime;
        this.agent = agent;
        this.hmmSounds = awareSounds;
    }
    private float currentDetectionTime = 0f;
    public override void OnEnter()
    {
        agent.isStopped = true;
        animator.CrossFade(AwareHash, crossFadeDuration);
        SoundFXManager.instance.PlaySoundFXClip(hmmSounds, controller.transform.position, 1f);
        
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