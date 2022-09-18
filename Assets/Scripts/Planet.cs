using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
public class Planet : MonoBehaviour
{
    [SerializeField] SolarBodyScriptable bodyScriptable;
    [SerializeField] GameObject miniDetailContainer;
    [SerializeField] TextMeshProUGUI planetName;
    [SerializeField] CinemachineVirtualCamera mainDynamicCam;
    [SerializeField] TextMeshProUGUI planetDistanceFromCamera;
    [SerializeField] GameObject listAllPlanetContainer;
    void Start()
    {
        if(miniDetailContainer)
            miniDetailContainer.SetActive(false);
    }
    private void OnMouseOver()
    {
        if(PovController.getIsInDynamicMode() && miniDetailContainer && planetName && planetDistanceFromCamera)
        {
            if(listAllPlanetContainer)
                listAllPlanetContainer.SetActive(false);
            miniDetailContainer.SetActive(true);
            planetName.text = "Solarbody Name: " + bodyScriptable.name;
            float distance = Vector3.Distance(transform.position, mainDynamicCam.transform.position);
            planetDistanceFromCamera.text = "Distance from current position: " + distance.ToString() + "miliKm";
        }

    }

    private void OnMouseExit()
    {
        miniDetailContainer.SetActive(false);
    }
    public SolarBodyScriptable getScriptableAssociated()
    {
        return bodyScriptable;
    }
}
 