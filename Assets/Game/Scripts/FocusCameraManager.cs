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

    #region SmoothDamp

    private Vector3 positionSmooth;

    #endregion

    void Awake()
    {
        rootTransform.position = targetTransform.position + offset;
    }

    private void Update()
    {
        rootTransform.position = Vector3.SmoothDamp(rootTransform.position, targetTransform.position + offset, ref positionSmooth, positionSmoothTime);
    }
}
