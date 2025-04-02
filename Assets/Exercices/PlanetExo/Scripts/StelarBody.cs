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
    [SerializeField] bool canMove = true;
    [SerializeField] Transform rotateOn;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = new Vector3(90.0f, 0.0f, 0.0f);
    Vector3 startPosition = Vector3.zero;

    public bool StopSimulation => stopSimulation;
    public bool CanMove => canMove;

    public void SetSimulation(bool _value) => stopSimulation = _value;
    public void SetCanMove(bool _value) => canMove = _value;

    private void Awake()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (!canMove) return;

        if (stopSimulation)
        {
            transform.position = startPosition;
            return;
        }
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
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawSphere(positionOffset, 0.2f);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawRay(positionOffset, transform.position);
    }

    public Vector3 GetNewPosition()
    {
        return new Vector3(Mathf.Cos(Time.time * moveSpeed), 0, Mathf.Sin(Time.time * moveSpeed)) * range + (rotateOn ? rotateOn.position : new Vector3(0.0f, 0.0f, 0.0f));
    }

    public Vector3 GetPositionOffset()
    {
        return transform.position + positionOffset;
    }

    public Vector3 GetRotationOffset()
    {
        return rotationOffset;
    }
}
