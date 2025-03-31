using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlanetData
{
    [field: SerializeField] public string Type { get; set; }
    [field: SerializeField] public string PlanetName { get; set; }
    [field: SerializeField] public string Diameter { get; set; }
    [field: SerializeField] public string Densite { get; set; }
    [field: SerializeField] public string Revolution { get; set; }
    [field: SerializeField] public string Rotation { get; set; }
    [field: SerializeField] public string Temperature { get; set; }
    [field: SerializeField] public string SatellitesCount { get; set; }

    public override string ToString()
    {
        return "Type = " + Type+ "\n" + "PlanetName = " + PlanetName + "\n" + "Diameter = " + Diameter + "\n" + "Densite = " + Densite + "\n" +
            "Revolution = " + Revolution + "\n" + "Rotation = " + Rotation + "\n" + "Temperature" + Temperature + "\n" + "SatellitesCount = " + SatellitesCount;
    }
}

public class PlanetComponent : MonoBehaviour
{
    public PlanetData? data = null;
    [SerializeField] StelarBody stelarBody = null;

    // Start is called before the first frame update
    void Start()
    {
        PlanetManager.Instance.Add(name,this);
        stelarBody = GetComponent<StelarBody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
