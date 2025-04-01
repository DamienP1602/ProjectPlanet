using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlanetManager : Singleton<PlanetManager>
{
    AllData data = new AllData();
    Dictionary<string,PlanetComponent> allPlanets = new();
    [SerializeField] LayerMask planetLayer = 0;
    [SerializeField] PlanetCanva canva = null;
    [SerializeField] CameraComponent cameraComp = null;

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
        //StartCoroutine(WebFetcher.Request(SetData));
        StartCoroutine(WebFetcher.Request((_data) => data = _data));

        canva.quitButton.onClick.AddListener(ResetTarget);
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Add(string _name,PlanetComponent _planet)
    {
        allPlanets[_name] = _planet;
        SetData();
    }

    public void SetData()
    {
        foreach (KeyValuePair <string,PlanetComponent> _planet in allPlanets)
        {
            foreach (PlanetData _planetData in data.results)
            {
                if (_planetData.PlanetName.Contains(_planet.Key))
                {
                    string _temperatureTemp = _planet.Value.data.Temperature;
                    _planet.Value.data = _planetData;

                    _planet.Value.data.Temperature = _planetData.VerifValue(_planetData.Temperature, _temperatureTemp);
                    continue;
                }
            }
            Debug.Log(_planet.Value.data.ToString());
        }
    }

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
                    ShowPlanetInfo(_result.collider.transform);
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

                ShowPlanetInfo(_result.collider.transform);
            }
        }
    }

    void ShowPlanetInfo(Transform _planet)
    {
        PlanetComponent _comp = allPlanets[_planet.parent.name];
        
        cameraComp.SetTarget(_comp);
        canva.gameObject.SetActive(true);
        canva.SetToCanva(_comp.data.PlanetName,_comp.data.ToString());
    }

    void ResetTarget()
    {
        canva.gameObject.SetActive(false);
        cameraComp.SetTarget(null);
        cameraComp.gameObject.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        cameraComp.gameObject.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }
}


public struct AllData
{
    public int total_count;
    public PlanetData[] results;
}