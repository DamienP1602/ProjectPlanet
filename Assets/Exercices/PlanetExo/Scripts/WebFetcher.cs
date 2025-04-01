using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebFetcher : MonoBehaviour
{
    public static readonly string WEB_URL = "https://www.datastro.eu/api/explore/v2.1/catalog/datasets/donnees-systeme-solaire-solar-system-data/records?select=type_d_astre_type_of_planet%20AS%20Type%2C%20planete_planet%20AS%20PlanetName%2C%20diametre_diameter_km%20AS%20Diameter%2C%20densite_density_kg_m3%20AS%20Densite%2C%20periode_de_revolution_jours_orbital_period_days%20AS%20Revolution%2C%20periode_de_rotation_rotation_period_h%20AS%20Rotation%2C%20temperature_moyenne_mean_temperature_degc%20AS%20Temperature%2C%20nombre_de_satellites_number_of_satellites%20AS%20SatellitesCount&limit=20";

    public static IEnumerator Request(Action<AllData> _action)
    {
        using (UnityWebRequest _request = UnityWebRequest.Get(WEB_URL))
        {
            yield return _request.SendWebRequest();

            AllData _data = new AllData();

            if (_request.result == UnityWebRequest.Result.ConnectionError || _request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error with the request : " + _request.error);
                _action(_data);
            }
            else
            {
                string _json = _request.downloadHandler.text;
                _data = JsonConvert.DeserializeObject<AllData>(_json);
                _action(_data);
            }
        }
    }
}
