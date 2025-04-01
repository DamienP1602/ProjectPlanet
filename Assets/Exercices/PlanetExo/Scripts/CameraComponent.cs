using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform springArm = null;
    [SerializeField] Camera render = null;

    [SerializeField] PlanetComponent target = null;

    public void SetTarget(PlanetComponent _target) => target = _target;

    private void Awake()
    {
        transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }
    private void Update()
    {
        if (target)
        {
            if (target.StelarBody.IsStopSimulation()) return;
            springArm.position = target.StelarBody.GetPositionOffset();
            springArm.eulerAngles = target.StelarBody.GetRotationOffset();
        }
    }
}
