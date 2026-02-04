using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private GunController gun;
    
    StateMachine _stateMachine;

    
    
    public bool playerInFOV;
    public bool playerDetected;
    private Transform playerTransform;
    private void Start()
    {
        _stateMachine = new StateMachine();


        var patrolState = new PatrolState(this, _animator, _agent, 5f);
        var engagedState = new EngagedState(this, _animator, _agent, playerTransform);
        var awareState = new AwareState(this, _animator);
        var combatState = new CombatState(this, _animator, gun, playerTransform);
        At(patrolState, awareState, new FuncPredicate(() => playerInFOV));
        At(awareState, patrolState, new FuncPredicate(() => !playerInFOV));
        
        
        Any(engagedState, new FuncPredicate(() => playerDetected));
        Any(combatState, new FuncPredicate(() => playerDetected && (Vector3.Distance(transform.position, playerTransform.position) <= gun.fireRange)));
        
        _stateMachine.SetState(patrolState);
    }

    void Update()
    {
        _stateMachine.Update();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
}