using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageSpawner : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager trackedManager;
    [SerializeField] TMP_Text planetNameText;
    [SerializeField] TMP_Text planetInfoText;
    [SerializeField] GameObject solarSystem;

    void Start()
    {
        trackedManager.trackedImagesChanged += SpawnImage;
    }

    void SpawnImage(ARTrackedImagesChangedEventArgs _args)
    {
        //Debug.Log(_args.ToString());
        foreach(ARTrackedImage _image in _args.added)
        {
            //print(_image.name);
            //Debug.Log(_image.name);
            //planetNameText.text = _image.referenceImage.name;
            DisplayInfoText(_image.referenceImage.name);
        }
    }

    void DisplayInfoText(string _name)
    {
        //List<GameObject> _allPlanets = solarSystem.GetComponentsInChildren<GameObject>().ToList<GameObject>();
        Dictionary<string, PlanetComponent> _allPlanets = PlanetManager.Instance.AllPlanets;        

        planetNameText.text = _allPlanets.Count.ToString();

        foreach(KeyValuePair<string, PlanetComponent> _planet in _allPlanets)
        {
            if(_planet.Key == _name)          
                _planet.Value.transform.parent.gameObject.SetActive(true);
        }

        
        //if (_prefab = allPlanets.Find(x => x.name == _name))
        //{
        //    //_body = Instantiate(_prefab);

        //    planetNameText.text = _name;
        //    planetInfoText.text = _body.GetComponentInChildren<PlanetComponent>().data.ToString();
        //    //planetInfoText.text = "J'affiche la";
        //    //PlanetManager.Instance.ShowPlanetInfo(_body.GetComponentInChildren<PlanetComponent>());
        //}



        //PlanetData _data = PlanetManager.Instance.GetPlanetData(_body.transform);
        //planetInfoText.text = _data.ToString();
    }
}
