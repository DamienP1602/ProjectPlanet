using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform springArm = null;
    [SerializeField] Camera render = null;

    [SerializeField] PlanetComponent target = null;

    public void SetTarget(PlanetComponent _target) => target = _target;

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
