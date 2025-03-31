using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StelarBody : MonoBehaviour
{
    [SerializeField] float range = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float rotateSpeed = 1.0f;
    [SerializeField] bool isRotPos = true;
    void Start()
    {
        
    }
    void Update()
    {        
        transform.position = new Vector3(Mathf.Cos(Time.time * moveSpeed), 0, Mathf.Sin(Time.time * moveSpeed)) * range;
        transform.Rotate(transform.up, Mathf.Cos(Time.deltaTime * rotateSpeed) * (isRotPos ? 1 : -1), Space.World);
    }
}
