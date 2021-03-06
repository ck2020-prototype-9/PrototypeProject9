﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[Serializable]
public class BackgroundCharacterControllerEvent : UnityEvent<BackgroundCharacterController> { }

public class BackgroundCharacterController : ResettableObject
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [Tooltip("")]
    [SerializeField] float speed = 3.5f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] bool isLoop;
    [SerializeField] BackgroundCharacterControllerEvent arriveEvent = new BackgroundCharacterControllerEvent();
    [SerializeField] bool isShowWaypointLine;

    private int waypointsStep = 0;

    enum ArriveStates
    {
        None = 0,
        Arrived = 1,
        ArrivedAndEventInvoked = 2
    }
    private ArriveStates arriveState; // 1 = arrived, 2 = arrivedEventInvoked

    public NavMeshAgent NavMeshAgent => navMeshAgent;

    public bool IsLoop
    {
        get => isLoop;
        set
        {
            isLoop = value;
            arriveState = 0;
            if (waypointsStep >= waypoints.Length)
            {
                waypointsStep = 0;
            }
        }
    }

    public bool IsArrived => 0 < arriveState;

    public BackgroundCharacterControllerEvent ArriveEvent => arriveEvent;

    public Transform[] GetWaypoints() => waypoints;
    public void SetWaypoints(Transform[] waypoints)
    {
        waypointsStep = 0;
        this.waypoints = waypoints;
    }

    protected override void Start()
    {
        base.Start();
        Initalize();
    }

    private void Initalize()
    {
        waypointsStep = 0;
        navMeshAgent.velocity = Vector3.zero;
        this.transform.position = waypoints[0].position;
        GotoNextPoint();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            if (arriveState == ArriveStates.Arrived)
            {
                arriveEvent?.Invoke(this);
                arriveState = ArriveStates.ArrivedAndEventInvoked;
            }
            else
            {
                GotoNextPoint();
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(this))
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
#endif

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
            if (waypointsStep >= waypoints.Length)
                navMeshAgent.autoBraking = true;
            else
                navMeshAgent.autoBraking = false;
        }
        else
        {
            arriveState = ArriveStates.Arrived;
        }
        return waypointsStep;
    }

    public override void StageReset()
    {
        base.StageReset();
        Initalize();
    }
}
