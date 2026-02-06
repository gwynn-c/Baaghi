using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LineOfSight : MonoBehaviour
{
    public float viewingRadius;
    public float meshResolution;
    [Range(0,360)]
    public float fieldOfView;
    public List<Transform> playersInView = new List<Transform>();
   
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask targetMask;
    private Transform playerTransform;

    private Enemy _controller;

    private void Awake()
    {
        _controller = GetComponent<Enemy>();
    }
    
    void Update()
    {
        FindVisibleTargets();
        
        DrawFieldOfView();
    }

    public void FindVisibleTargets()
    {
        playersInView.Clear();
        var results = Physics.OverlapSphere(transform.position, viewingRadius, targetMask);
        for (int i = 0; i < results.Length; i++)
        {
            Transform target = results[i].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) <= fieldOfView / 2)
            {   
                
                var distance = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                        playersInView.Add(target);
                if(playersInView.Count > 0) playerTransform = target;
            }
        }
    }

    public bool IsPlayerVisible()
    {
        
        return playersInView.Count > 0;
    }

 
    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(fieldOfView * meshResolution);
        float stepAngleSize = fieldOfView / stepCount;

        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - fieldOfView / 2 + stepAngleSize * i;
        }
    }
    
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}