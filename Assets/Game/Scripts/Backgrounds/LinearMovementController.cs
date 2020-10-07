using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class LinearMovementController : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] bool isLoop;
    [SerializeField] float speed;
    [SerializeField] bool isShowWaypointLine;

    Transform thisTransform;

    Tween tween;

    private void Start()
    {
        thisTransform = this.transform;

        thisTransform.position = from.position;
        thisTransform.LookAt(to);

        // duration 계산
        float duration = Vector3.Distance(from.position, to.position) / speed;

        tween = thisTransform.DOMove(to.position, duration)
            .SetEase(Ease.Linear);

        if (isLoop)
        {
            tween.SetLoops(-1);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(this))
            this.transform.position = from.position;
        if (isShowWaypointLine)
        {
            Handles.color = Color.cyan;
            Handles.DrawLine(from.position, to.position);
        }
    }
#endif
}