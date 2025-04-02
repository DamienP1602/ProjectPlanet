using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.MagicLeap;

using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class RaycastMover : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform solarSystemTransform;

    private Vector2 touchPosition;
    private bool needToMove = false;

    private bool IsValid => planeManager && raycastManager && solarSystemTransform;

    void Update()
    {
        if (!IsValid) return;

        if(TryGetTouchPosition(out Vector2 _touchPosition))
        {
            touchPosition = _touchPosition;
            needToMove = true;
        }

        if(needToMove)
            MoveSystem(touchPosition);
    }

    private bool TryGetTouchPosition(out Vector2 _touchPosition)
    {
        if(InputTouch.activeTouches.Count > 0 && InputTouch.activeTouches[0].began)
        {
            _touchPosition = InputTouch.activeTouches[0].screenPosition;
            return true;
        }

        _touchPosition = default;
        return false;
    }

    private void MoveSystem(Vector2 _touchPosition)
    {
        List<ARRaycastHit> _outResults = new List<ARRaycastHit>();
        if(raycastManager.Raycast(_touchPosition, _outResults, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            Pose _pose = _outResults[0].pose;
            Vector3 _current = Vector3.MoveTowards(solarSystemTransform.position, _pose.position, Time.deltaTime);
            if (Vector3.Distance(_current, _pose.position) < 1.0f)
                needToMove = false;
        }
    }
}
