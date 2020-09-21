using UnityEngine;
using System.Collections;

public class ResettableObject : MonoBehaviour, IStageResettable
{
    Transform thisTransform;
    Rigidbody thisRigidbody;

    Vector3 initLocalPosition;
    Quaternion initLocalRotation;
    Vector3 initLocalScale;

    protected virtual void Awake()
    {
        thisTransform = transform;
        thisRigidbody = GetComponent<Rigidbody>();

        initLocalPosition = thisTransform.localPosition;
        initLocalRotation = thisTransform.localRotation;
        initLocalScale = thisTransform.localScale;
    }

    protected virtual void Start()
    {
        GameStageManager.Instance.RegisterResettableObject(this);
    }

    public virtual void StageReset()
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