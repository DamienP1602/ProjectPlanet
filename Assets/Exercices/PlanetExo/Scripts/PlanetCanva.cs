using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PanelEnum
{
    INFO_PANEL,
    MAIN_PANEL,
    NONE
}

public class PlanetCanva : MonoBehaviour
{
    [Header("InfoPanel")]
    [SerializeField] GameObject infoPanel = null;
    [SerializeField] TMP_Text title = null;
    [SerializeField] TMP_Text info = null;
    [SerializeField] Button quitButton = null;

    [Header("MainPanel")]
    [SerializeField] GameObject mainPanel = null;
    [SerializeField] Button stopMotionPlanel = null;
    [SerializeField] Button sortPlanel = null;

    public GameObject InfoPanel => infoPanel;
    public GameObject MainPanel => mainPanel;

    public Button QuitButton => quitButton;
    public Button StopMotionButton => stopMotionPlanel;
    public Button SortButton => sortPlanel;

    public void SetToCanva(string _planetName, string _info)
    {
        title.text = _planetName;
        info.text = _info;
    }

    public void ChangeCurrentPanel(PanelEnum _enum)
    {
        if (_enum == PanelEnum.NONE)
        {
            infoPanel.SetActive(false);
            mainPanel.SetActive(false);
        }
        else if (_enum == PanelEnum.MAIN_PANEL)
        {
            infoPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        else if (_enum == PanelEnum.INFO_PANEL)
        {
            mainPanel.SetActive(false);
            infoPanel.SetActive(true);
        }
        gameObject.SetActive(true);
    }
}
