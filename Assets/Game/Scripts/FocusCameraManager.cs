using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraManager : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector3 offset;
    [Space]
    [SerializeField] Transform rootTransform;
    [SerializeField] float positionSmoothTime = 0.2f;

    public Transform TargetTransform
    {
        get => targetTransform;
        set
        {
            targetTransform = value;
            rootTransform.position = targetTransform.position + offset;
        }
    }

    #region SmoothDamp

    private Vector3 positionSmooth;

    #endregion

    void Awake()
    {
        rootTransform.position = targetTransform.position + offset;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            rootTransform.position = Vector3.SmoothDamp(rootTransform.position, targetTransform.position + offset, ref positionSmooth, positionSmoothTime);
        }
    }
}
