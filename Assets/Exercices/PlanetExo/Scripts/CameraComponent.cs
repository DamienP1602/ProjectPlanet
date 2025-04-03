using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] Transform springArm = null;
    [SerializeField] Camera render = null;

    [SerializeField] PlanetComponent target = null;

    public void SetTarget(PlanetComponent _target) => target = _target;

    public bool IsNear => Vector3.Distance(target.StelarBody.GetPositionOffset(), transform.position) < 0.01f;

    private void Awake()
    {
        transform.position = new Vector3(0.0f, 15.0f, 0.0f);
        transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }

    private void Update()
    {
        print("DEBUG => CAMERA COMPONENT  = " + transform.position);

        if (target)
        {
            if (!IsNear)
            {
                Vector3 _direction = target.transform.position - transform.position;
                _direction.Normalize();
                Quaternion _lookAt = Quaternion.LookRotation(_direction);

                springArm.position = Vector3.Lerp(springArm.position, target.StelarBody.GetPositionOffset(), Time.deltaTime * 5.0f);
                springArm.rotation = Quaternion.Lerp(springArm.rotation, _lookAt, Time.deltaTime * 5.0f);

            }
        }
    }
}
