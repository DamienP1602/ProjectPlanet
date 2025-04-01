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
        return "Type = " + VerifValue(Type) + "\n" + "Diameter = " + VerifValue(Diameter) + "\n" + "Densite = " + VerifValue(Densite) + "\n" +
            "Revolution = " + VerifValue(Revolution) + "\n" + "Rotation = " + VerifValue(Rotation) + "\n" + 
            "Average Temperature = " + VerifValue(Temperature) + "\n" + "Amount of Satellites = " + VerifValue(SatellitesCount);
    }

    public string VerifValue(string _str, string _toReplace = "0")
    {
        return string.IsNullOrEmpty(_str) ? _toReplace : _str;
    }
}

public class PlanetComponent : MonoBehaviour
{
    public PlanetData? data = null;
    [SerializeField] StelarBody stelarBody = null;
    public StelarBody StelarBody => stelarBody;

    // Start is called before the first frame update
    void Start()
    {
        PlanetManager.Instance.Add(transform.parent.name,this);
        stelarBody = GetComponent<StelarBody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
