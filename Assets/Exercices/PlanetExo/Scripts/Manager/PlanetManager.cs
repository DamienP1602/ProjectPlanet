using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : Singleton<PlanetManager>
{
    AllData data = new AllData();
    Dictionary<string,PlanetComponent> allPlanets = new();

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WebFetcher.Request(SetData));
        StartCoroutine(WebFetcher.Request((_data) => data = _data));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add(string _name,PlanetComponent _planet)
    {
        allPlanets[_name] = _planet;
        //SetData();
    }

    public void SetData()
    {
        int _size = allPlanets.Count;

        foreach (KeyValuePair <string,PlanetComponent> _planet in allPlanets)
        {
            foreach (PlanetData _planetData in data.results)
            {
                if (_planetData.PlanetName.Contains(_planet.Key))
                {
                    _planet.Value.data = _planetData;
                    continue;
                }
            }
            Debug.Log(_planet.Value.data.ToString());
        }
    }
}


public struct AllData
{
    public int total_count;
    public PlanetData[] results;
}