using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Networking;
using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlanetManager : Singleton<PlanetManager>
{
    AllData data = new AllData();
    [SerializeField] List<PlanetComponent> allPlanets = new();
    [SerializeField] LayerMask planetLayer = 0;
    [SerializeField] PlanetCanva canva = null;
    [SerializeField] CameraComponent cameraComp = null;
    [SerializeField] GameObject solarSystem = null;

    public List<PlanetComponent> AllPlanets => allPlanets;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canva.quitButton.onClick.AddListener(ResetTarget);
        SearchPlanets();
    }

// Update is called once per frame
void Update()
    {
        Interact();
    }

    void SearchPlanets()
    {
        allPlanets = solarSystem.GetComponentsInChildren<PlanetComponent>(true).ToList();
    }

    //public void SetData(string _name, PlanetComponent _planet)
    //{
    //    foreach (PlanetData _planetData in data.results)
    //    {
    //        if (_planetData.PlanetName.Contains(_name))
    //        {
    //            string _temperatureTemp = _planet.data.Temperature;
    //            _planet.data = _planetData;

    //            _planet.data.Temperature = _planetData.VerifValue(_planetData.Temperature, _temperatureTemp);
    //            continue;
    //        }
    //    }
    //    Debug.Log(_planet.data.ToString());
    //}

    void Interact()
    {
        if (InputTouch.activeTouches.Count > 0)
        {
            InputTouch _touch = InputTouch.activeTouches[0];

            if (_touch.began)
            {
                Ray _ray = Camera.main.ScreenPointToRay(_touch.screenPosition);
                RaycastHit _result;

                if (Physics.Raycast(_ray, out _result, 100.0f, planetLayer))
                {
                    PlanetComponent _planetComponent = _result.collider.GetComponent<PlanetComponent>();
                    ShowPlanetInfo(_planetComponent);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _result;
            Debug.DrawRay(_ray.origin,_ray.direction * 100.0f);

            if (Physics.Raycast(_ray, out _result, 100.0f, planetLayer))
            {
                PlanetComponent _planetComponent = _result.collider.GetComponent<PlanetComponent>();
                ShowPlanetInfo(_planetComponent);
            }
        }
    }

    public void ShowPlanetInfo(PlanetComponent _planet)
    {
        cameraComp.SetTarget(_planet);
        canva.gameObject.SetActive(true);
        canva.SetToCanva(_planet.data.PlanetName, _planet.data.ToString());
    }

    void ResetTarget()
    {
        canva.gameObject.SetActive(false);
        cameraComp.SetTarget(null);

        cameraComp.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        cameraComp.transform.eulerAngles = new Vector3(90.0f,0.0f,0.0f);
    }
}


public struct AllData
{
    public int total_count;
    public PlanetData[] results;
}