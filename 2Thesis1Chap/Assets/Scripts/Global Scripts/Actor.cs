using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Actor : MonoBehaviour
{
    //References
    private NavMeshAgent  _agent;
    private Animator _animator;
    public LineOfSight LineOfSight { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        LineOfSight = GetComponent<LineOfSight>();
    }
    

    #region Patrol

    //Patrol Function
    //Patrolling Variables, for the time to start patrolling, delay patrolling when reaching destination and the radius of which the actor can move
    [SerializeField] float patrolTimer;
    [SerializeField] float patrolDelay;
    [SerializeField] float patrolRadius;

    public bool playerInFOV;
    
    private static readonly int MoveSpeed = Animator.StringToHash("Speed");

    public void Patrol()
    {
        if (playerInFOV)
        {
            StartCoroutine(PlayerDetection());
        }
        else
        {
            _agent.isStopped = false;
            StartCoroutine(LineOfSight.PlayerIsVisible());
            _animator.SetFloat(MoveSpeed, _agent.velocity.magnitude);
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= patrolDelay)
                {
                    _agent.SetDestination(GetPatrolPosition(transform.position, patrolRadius, -1));
                    patrolTimer = 0f;
                }
            }
        }

    }

    Vector3 GetPatrolPosition(Vector3 origin, float distance, int layerMask){

        Vector3 patrolDirection = Random.insideUnitSphere * distance;
        patrolDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(patrolDirection, out navHit, distance, layerMask);
        return navHit.position;
    }

    #endregion
    private Transform _target;
    public bool PlayerDetected { get; private set; }
    [SerializeField] private float detectionTime;

    
    private IEnumerator PlayerDetection()
    {

        while (playerInFOV && !PlayerDetected)
        {
            _agent.isStopped = true;
            detectionTime -= Time.deltaTime;
            yield return new WaitUntil(() => detectionTime <= 0);
            PlayerDetected = true;
            playerInFOV = false;
            detectionTime = 2f;
            StopAllCoroutines();
        }

        yield return null;
    }
    
   


}