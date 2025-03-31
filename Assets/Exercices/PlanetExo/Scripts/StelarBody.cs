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
    [SerializeField] Transform rotateOn;
    void Update()
    {
        transform.Rotate(transform.up, Mathf.Cos(Time.deltaTime * rotateSpeed) * GetRotationDirection(), Space.World);
        if (rotateOn == null) return;
        transform.position = new Vector3(Mathf.Cos(Time.time * moveSpeed), 0, Mathf.Sin(Time.time * moveSpeed)) * range + rotateOn.position;
    }

    float GetRotationDirection()
    {
        return isClockwise ? 1.0f : -1.0f;
    }
}
