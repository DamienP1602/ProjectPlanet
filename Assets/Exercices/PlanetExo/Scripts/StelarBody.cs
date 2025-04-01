using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StelarBody : MonoBehaviour
{
    [SerializeField] float range = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float rotateSpeed = 1.0f;
    [SerializeField] bool isClockwise = true;
    [SerializeField] bool stopSimulation = false;
    [SerializeField] Transform rotateOn;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = Vector3.zero;
    void Update()
    {
        //Camera.main.transform.parent.position = positionOffset;
        //Camera.main.transform.parent.eulerAngles = rotationOffset;
        if (stopSimulation) return;
        transform.Rotate(transform.up, Mathf.Cos(Time.deltaTime * rotateSpeed) * GetRotationDirection(), Space.World);
        if (rotateOn == null) return;
        transform.position = GetNewPosition();
    }

    float GetRotationDirection()
    {
        return isClockwise ? 1.0f : -1.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(positionOffset + GetNewPosition(), 0.2f);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(positionOffset, transform.position);
    }
    private Vector3 GetNewPosition()
    {
        return new Vector3(Mathf.Cos(Time.time * moveSpeed), 0, Mathf.Sin(Time.time * moveSpeed)) * range + rotateOn.position;
    }
}
