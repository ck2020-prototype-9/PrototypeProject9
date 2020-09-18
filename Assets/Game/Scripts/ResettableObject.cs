using UnityEngine;
using System.Collections;

public class ResettableObject : MonoBehaviour, IStageResettable
{
    Transform thisTransform;
    Rigidbody thisRigidbody;

    Vector3 initLocalPosition;
    Quaternion initLocalRotation;
    Vector3 initLocalScale;

    void Awake()
    {
        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();

        initLocalPosition = thisTransform.localPosition;
        initLocalRotation = thisTransform.localRotation;
        initLocalScale = thisTransform.localScale;
    }

    void Start()
    {
        GameStageManager.Instance.RegisterResettableObject(this);
    }

    public void StageReset()
    {
        thisTransform.localPosition = initLocalPosition;
        thisTransform.localRotation = initLocalRotation;
        thisTransform.localScale = initLocalScale;

        if (thisRigidbody != null)
        {
            thisRigidbody.angularVelocity = thisRigidbody.velocity = Vector3.zero;
        }
    }
}