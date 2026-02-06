using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private GunController gun;
    private HealthController healthController;
    public Transform lastKnownPosition;
    public Transform equippedGunSlot;
    StateMachine _stateMachine;

    public bool IsEnemyIdle = true;
    public bool IsDead = false;
    
    
    public float patrolRadius;
    public bool playerInFOV;
    public bool playerDetected;

    public float detectionTime = 3f;
    
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        gun = equippedGunSlot.GetComponentInChildren<GunController>();
        healthController = GetComponent<HealthController>();
    }
    
    private void Start()
    {
        _stateMachine = new StateMachine();


        var idleState = new IdleState(this, _animator);
        var patrolState = new PatrolState(this, _animator, _agent, patrolRadius);
        var awareState = new AwareState(this, _animator,_agent, detectionTime);
        var combatState = new CombatState(this, _animator, lastKnownPosition, gun, gun.fireRange);
        var deathState = new DeathState(this, _animator);
        
        At(patrolState, awareState, new FuncPredicate(() => playerInFOV));
        At(awareState, patrolState, new FuncPredicate(() => !playerInFOV));
        At(idleState, patrolState, new FuncPredicate(() => !IsEnemyIdle));
        
        Any(deathState,new FuncPredicate(() => IsDead));
        Any(idleState, new FuncPredicate(() => IsEnemyIdle));
        
        At(awareState, combatState, new FuncPredicate(() => playerDetected));
        _stateMachine.SetState(idleState);
    }
    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    void Update()
    {
        _stateMachine.Update();
        playerInFOV = CheckLineOfSight();
        IsDead = healthController.GetIsDead();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    

    public bool CheckLineOfSight()
    {
        return GetComponent<LineOfSight>().IsPlayerVisible();
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(transform.position), FootstepAudioVolume);
            }
        }
    }


    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }
    
}