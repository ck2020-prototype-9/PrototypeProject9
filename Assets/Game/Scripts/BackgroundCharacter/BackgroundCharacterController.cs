using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[Serializable]
public class BackgroundCharacterControllerEvent : UnityEvent<BackgroundCharacterController> { }

public class BackgroundCharacterController : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [Tooltip("")]
    [SerializeField] float speed = 3.5f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] bool isLoop;
    [SerializeField] BackgroundCharacterControllerEvent arriveEvent = new BackgroundCharacterControllerEvent();
    [SerializeField] bool isShowWaypointLine;

    private int waypointsStep = 0;

    public NavMeshAgent NavMeshAgent => navMeshAgent;
    public bool IsLoop
    {
        get => isLoop;
        set
        {
            isLoop = value;
            if (waypointsStep >= waypoints.Length)
            {
                waypointsStep = 0;
            }
        }
    }
    public BackgroundCharacterControllerEvent ArriveEvent => arriveEvent;

    public Transform[] GetWaypoints() => waypoints;
    public void SetWaypoints(Transform[] waypoints)
    {
        waypointsStep = 0;
        this.waypoints = waypoints;
    }

    private void Start()
    {
        this.transform.position = waypoints[0].position;
        GotoNextPoint();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    private void OnDrawGizmos()
    {
        this.transform.position = waypoints[0].position;
        if (isShowWaypointLine)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                int j = (i + 1) % waypoints.Length;
                
                if (j != 0 || isLoop)
                {
                    Handles.color = Color.green;
                    Handles.DrawLine(waypoints[i].position, waypoints[j].position);
                    //Gizmos.color = Color.green;
                    //Gizmos.DrawLine(waypoints[i].position, waypoints[j].position);
                }
            }
        }
    }

    private void GotoNextPoint()
    {
        NextStep();
        GotoCurrentPoint();
    }

    private void GotoCurrentPoint()
    {
        if (waypointsStep < waypoints.Length)
        {
            navMeshAgent.SetDestination(waypoints[waypointsStep].position);
        }
    }

    private int NextStep()
    {
        if (IsLoop)
        {
            waypointsStep = (waypointsStep + 1) % waypoints.Length;
            navMeshAgent.autoBraking = false;
        }
        else if (waypointsStep < waypoints.Length)
        {
            waypointsStep += 1;
            navMeshAgent.autoBraking = false;
        }
        else
        {
            navMeshAgent.autoBraking = true;
        }
        return waypointsStep;
    }
}
