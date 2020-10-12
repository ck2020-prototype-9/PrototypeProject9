using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] Vector3 axis = Vector3.forward;

    float currentAngle;
    void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.AngleAxis(currentAngle, axis);
        transform.Rotate(axis * rotationSpeed * Time.deltaTime);
    }
}