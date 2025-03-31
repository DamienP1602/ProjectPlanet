using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlanetData
{
    public string Type { get; set; }
    public string PlanetName { get; set; }
    public string Diameter { get; set; }
    public string Densite { get; set; }
    public string Revolution { get; set; }
    public string Rotation { get; set; }
    public string Temperature { get; set; }
    public string SatellitesCount { get; set; }

    public override string ToString()
    {
        return "Type = " + Type+ "\n" + "PlanetName = " + PlanetName + "\n" + "Diameter = " + Diameter + "\n" + "Densite = " + Densite + "\n" +
            "Revolution = " + Revolution + "\n" + "Rotation = " + Rotation + "\n" + "Temperature" + Temperature + "\n" + "SatellitesCount = " + SatellitesCount;
    }
}

public class PlanetComponent : MonoBehaviour
{
    public PlanetData? data = null;
    // Start is called before the first frame update
    void Start()
    {
        PlanetManager.Instance.Add(name,this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
