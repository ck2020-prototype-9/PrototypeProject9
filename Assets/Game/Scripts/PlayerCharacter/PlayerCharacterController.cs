using System;
using System.Collections;
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
    [SerializeField] float legSpeedScale = 0.5f;
    [SerializeField] float legSmoothTime = 0.1f;
    [SerializeField] float legResetSpeed = 45f;
    [SerializeField] float legMinAngle = -90;
    [SerializeField] float legMaxAngle = 90;

    [Header("Balance")]
    [SerializeField] float balanceControlScale = 171f;
    [SerializeField] float balanceControlSmoothTime = 0.05f;

    [Header("Auto Adjust")]
    [SerializeField] float xAdjustFactor = 1422.64f;
    [SerializeField] float yAdjustFactor = 100;
    [SerializeField] float zAdjustFactor = 711.32f;
    [SerializeField] float yRotateAdjustFactor = 20;

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
        //Debug.Log("leftLeg");
    }

    public void OnRightLegInput(InputAction.CallbackContext context)
    {
        rightLegInput = context.ReadValueAsButton();
        //Debug.Log("rightLeg");
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
            targetLeftLegRotation += mouseYDelta * legSpeedScale;
        }
        targetLeftLegRotation = Mathf.Clamp(targetLeftLegRotation, legMinAngle, legMaxAngle);

        // 오른쪽 다리
        if (rightLegInput)
        {
            targetRightLegRotation += mouseYDelta * legSpeedScale;
        }
        targetRightLegRotation = Mathf.Clamp(targetRightLegRotation, legMinAngle, legMaxAngle);

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

        if (!GameManager.Instance.IsGameOver)
        {
            if (characterBalanceInput.sqrMagnitude > DeadZone * DeadZone)
            {
                targetBalanceVelocity = characterBalanceInput * balanceControlScale * Time.deltaTime;
            }
            else
            {
                targetBalanceVelocity = Vector2.zero;
            }

            var forward = bodyRigidBody.transform.forward;
            var right = bodyRigidBody.transform.right;
            //Debug.Log($"forward: {forward}, forward.z: {forward.z}");
            //Debug.Log($"right: {right}, right.x: {right.x}");

            currentBalanceVelocity = Vector2.SmoothDamp(currentBalanceVelocity, targetBalanceVelocity, ref balanceVelocitySmooth, balanceControlSmoothTime);

            bodyRigidBody.AddForce(new Vector3(currentBalanceVelocity.x * right.x + currentBalanceVelocity.y * forward.x, 0, currentBalanceVelocity.y * forward.z + currentBalanceVelocity.x * right.z));
        }
    }

    private void AutoAdjustUpdate()
    {
        // 보정
        // 각도 만큼 좀더 밀어주기
        var up = bodyRigidBody.transform.up;
        if (!GameManager.Instance.IsGameOver)
        {
            bodyRigidBody.AddForce(new Vector3(-up.x * xAdjustFactor, yAdjustFactor, -up.z * zAdjustFactor) * Time.deltaTime);


            // 회전 방향 보정
            var forward = bodyRigidBody.transform.forward;
            //Debug.Log(forward);
            var rotation = bodyRigidBody.rotation;

            var eulerAngle = rotation.eulerAngles;
            float yAngle = eulerAngle.y;
            if (yAngle > 180)
            {
                yAngle -= 360;
            }

            bodyRigidBody.AddTorque(up * -yAngle * yRotateAdjustFactor * Time.deltaTime);
        }
        else
        {
            Debug.Log("game over");
        }
    }
}
