using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

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

    private void OnEnable()//
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Start is called before the first frame updateS
    void Start()
    {
        InitCanva();
        SearchPlanets();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void InitCanva()
    {
        canva.QuitButton.onClick.AddListener(ResetTarget);

        canva.StopMotionButton.onClick.AddListener(ToggleMovement);

        canva.SortButton.onClick.AddListener(SortPlanet);
    }

    void SearchPlanets()
    {
        allPlanets = solarSystem.GetComponentsInChildren<PlanetComponent>(true).ToList();
    }

    public PlanetComponent GetPlanetByName(string _name)
    {
        foreach (PlanetComponent _planet in AllPlanets)
        {
            if (_planet.data.PlanetName == _name)
                return _planet;
        }
        return null;
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
            Debug.DrawRay(_ray.origin, _ray.direction * 100.0f);

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
        canva.ChangeCurrentPanel(PanelEnum.INFO_PANEL);
        canva.SetToCanva(_planet.data.PlanetName, _planet.data.ToString());
    }

    void ResetTarget()
    {
        canva.gameObject.SetActive(false);
        cameraComp.SetTarget(null);
        canva.ChangeCurrentPanel(PanelEnum.MAIN_PANEL);

        cameraComp.transform.position = new Vector3(0.0f, 15.0f, 0.0f) + GetPlanetByName("Sun").transform.position;
        cameraComp.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }

    void ToggleMovement()
    {
        foreach (PlanetComponent _planet in allPlanets)
        {
            if (_planet.StelarBody.StopSimulation)
                continue;

            _planet.StelarBody.SetCanMove(!_planet.StelarBody.CanMove);
        }
    }

    void SortPlanet()
    {
        foreach (PlanetComponent _planet in allPlanets)
        {
            if (!_planet.StelarBody.CanMove)
                continue;

            _planet.StelarBody.SetSimulation(!_planet.StelarBody.StopSimulation);
        }
    }
}


public struct AllData
{
    public int total_count;
    public PlanetData[] results;
}