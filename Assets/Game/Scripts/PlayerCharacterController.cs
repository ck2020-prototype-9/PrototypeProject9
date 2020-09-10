﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] Rigidbody bodyRigidBody;
    [SerializeField] Rigidbody leftLegRigidBody;
    [SerializeField] Rigidbody rightLegRigidBody;

    [Header("Hinge Joint")]
    [SerializeField] HingeJoint leftLegJoint;
    [SerializeField] HingeJoint rightLegJoint;

    [Header("Leg")]
    [SerializeField] float legSpeedScale = 10f;
    [SerializeField] float legSmoothTime = 0.2f;
    [SerializeField] float legResetSpeed = 10f;

    [Header("Balance")]
    [SerializeField] float balanceControlScale = 1f;
    [SerializeField] float balanceControlSmoothTime = 0.2f;

    [Header("Auto Adjust")]
    [Range(0, 10)]
    [SerializeField] float adjustFactor = 2;

    #region Input Values
    bool leftLegInput;
    bool rightLegInput;
    float mouseYDelta;
    Vector2 characterBalanceInput;
    #endregion

    #region smoothDamp Values
    // 왼쪽 다리
    float targetLeftLegRotation;
    float currentLeftLegRotation;
    float leftLegRotationSmooth;

    // 오른쪽 다리
    float targetRightLegRotation;
    float currentRightLegRotation;
    float rightLegRotationSmooth;

    // 밸런스
    Vector2 targetBalanceVelocity;
    Vector2 currentBalanceVelocity;
    Vector2 balanceVelocitySmooth;
    #endregion

    #region Input Events
    public void OnLeftLegInput(InputAction.CallbackContext context)
    {
        leftLegInput = context.ReadValueAsButton();
        Debug.Log("leftLeg");
    }

    public void OnRightLegInput(InputAction.CallbackContext context)
    {
        rightLegInput = context.ReadValueAsButton();
        Debug.Log("rightLeg");
    }

    public void OnMouseYDeltaInput(InputAction.CallbackContext context)
    {
        mouseYDelta = context.ReadValue<float>();
    }

    public void OnCharacterBalanceInput(InputAction.CallbackContext context)
    {
        characterBalanceInput = context.ReadValue<Vector2>();
    }
    #endregion

    private void Update()
    {
        LegUpdate();
        BalanceUpdate();
        AutoAdjustUpdate();
    }

    private void LegUpdate()
    {
        // 왼쪽 다리
        if (leftLegInput)
        {
            targetLeftLegRotation += mouseYDelta * legSpeedScale * Time.deltaTime;
        }
        // 왼쪽 다리 리셋
        if (targetLeftLegRotation > 0.1f)
        {
            targetLeftLegRotation -= legResetSpeed * Time.deltaTime;
        }
        else if (targetLeftLegRotation < -0.1f)
        {
            targetLeftLegRotation += legResetSpeed * Time.deltaTime;
        }
        currentLeftLegRotation = Mathf.SmoothDamp(currentLeftLegRotation, targetLeftLegRotation, ref leftLegRotationSmooth, legSmoothTime);

        // 오른쪽 다리
        if (rightLegInput)
        {
            targetRightLegRotation += mouseYDelta * legSpeedScale * Time.deltaTime;
        }
        // 오른쪽 다리 리셋
        if (targetRightLegRotation > 0.1f)
        {
            targetRightLegRotation -= legResetSpeed * Time.deltaTime;
        }
        else if (targetRightLegRotation < -0.1f)
        {
            targetRightLegRotation += legResetSpeed * Time.deltaTime;
        }
        currentRightLegRotation = Mathf.SmoothDamp(currentRightLegRotation, targetRightLegRotation, ref rightLegRotationSmooth, legSmoothTime);

        // 값 반영
        var leftLegSpring = leftLegJoint.spring;
        var rightLegSpring = rightLegJoint.spring;

        leftLegSpring.targetPosition = currentLeftLegRotation;
        rightLegSpring.targetPosition = currentRightLegRotation;

        leftLegJoint.spring = leftLegSpring;
        rightLegJoint.spring = rightLegSpring;
    }

    private void BalanceUpdate()
    {
        const float DeadZone = 0.1f;

        if (characterBalanceInput.sqrMagnitude > DeadZone * DeadZone)
        {
            targetBalanceVelocity = characterBalanceInput * balanceControlScale;
        }
        else
        {
            targetBalanceVelocity = Vector2.zero;
        }

        currentBalanceVelocity = Vector2.SmoothDamp(currentBalanceVelocity, targetBalanceVelocity, ref balanceVelocitySmooth, balanceControlSmoothTime);

        bodyRigidBody.AddForce(new Vector3(currentBalanceVelocity.x, 0, currentBalanceVelocity.y));
    }

    private void AutoAdjustUpdate()
    {
        // 보정
        // 각도 만큼 좀더 밀어주기
        var up = bodyRigidBody.transform.up;
        if (up.y > 0.4)
        {
            bodyRigidBody.AddForce(new Vector3(-up.x * adjustFactor, 0, -up.z * adjustFactor));
        }
        else
        {
            Debug.Log("game over");
        }

        // 회전 방향 보정
    }
}
