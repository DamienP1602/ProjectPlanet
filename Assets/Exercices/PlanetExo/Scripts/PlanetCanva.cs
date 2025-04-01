using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetCanva : MonoBehaviour
{
    [SerializeField] TMP_Text title = null;
    [SerializeField] TMP_Text info = null;
    public Button quitButton = null;

    public void SetToCanva(string _planetName, string _info)
    {
        title.text = _planetName;
        info.text = _info;
    }
}
