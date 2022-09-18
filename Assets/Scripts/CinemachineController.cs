using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
public class CinemachineController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainDynamicCam;
    [SerializeField] CinemachineVirtualCamera mainStaticCam;
    [SerializeField] CinemachineVirtualCamera closeCam;
    [SerializeField] float xAdjusmentForSolarCam = 10f;
    [SerializeField] float zAdjustmentForSolarCam = 10f;
    [SerializeField] GameObject detailContainer;
    [SerializeField] GameObject allPlanetListContainer;
    [SerializeField] TextMeshProUGUI planetName;
    [SerializeField] TextMeshProUGUI planetRadius;
    [SerializeField] TextMeshProUGUI planetMass;
    [SerializeField] TextMeshProUGUI planetOrbitRadius;
    [SerializeField] TextMeshProUGUI planetDescription;
    [SerializeField] TextMeshProUGUI planetMoons;
    [SerializeField] GameObject target;
    bool isInDynamicMode = false;
    public static CinemachineController instance;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        instance = this;
    }
    private void Start()
    {
        if(!mainDynamicCam || !mainStaticCam || !closeCam )
        {
            Debug.Log("Cameras not assigned in CinemachineController");
            return;
        }
        if(!planetDescription || !planetMass || !planetName || !planetOrbitRadius || !planetRadius || !planetMoons || !detailContainer || 
        !allPlanetListContainer)
        {
            Debug.Log("Planet Info not assigned");
            return;
        }
        detailContainer.SetActive(false);
        target.SetActive(false);
        allPlanetListContainer.SetActive(true);
    }
    public void focusOnObjectUsingUI(int planetId)
    {
        if(!closeCam)
        {
            Debug.Log("CloseCam not assigned in CinemachineController");
        }
        Planet[] planets = FindObjectsOfType<Planet>();
        foreach( Planet p in planets)
        {
            if(p.getScriptableAssociated().id == planetId)
            {
                closeCam.Follow = p.transform;
                closeCam.LookAt = p.transform;
                CinemachineCameraOffset cameraOffset = closeCam.GetComponent<CinemachineCameraOffset>();
                if (cameraOffset)
                    cameraOffset.m_Offset = new Vector3((p.transform.localScale.x / 2) + xAdjusmentForSolarCam, 0f,
                    -p.transform.localScale.z + zAdjustmentForSolarCam);
                else
                    Debug.LogError("Coudn't find offset");
                closeCam.Priority = 5;

                if (!detailContainer || !allPlanetListContainer)
                {
                    Debug.Log("Container not assigned");
                    break;
                }
                Cursor.lockState = CursorLockMode.None;
                FillDetailContainer(p);
                if(target)
                    target.SetActive(false);
                break;
            }
        }
    }

    private void FillDetailContainer(Planet p)
    {
        allPlanetListContainer.SetActive(false);
        detailContainer.SetActive(true);
        SolarBodyScriptable bodyScriptable = p.getScriptableAssociated();
        planetName.text = bodyScriptable.name.ToString();
        planetRadius.text = "Radius: " + bodyScriptable.radius.ToString();
        planetMass.text = "Mass: " + bodyScriptable.mass.ToString();
        planetDescription.text = "Special Fact: " + bodyScriptable.description.ToString();
        planetMoons.text = "No. of moons: " + bodyScriptable.numberOfMoons.ToString();
        planetOrbitRadius.text = "Orbit Radius: " + bodyScriptable.distanceFromSun.ToString();
    }

    public void returnToNormal()
    {
        if(isInDynamicMode)
            Cursor.lockState = CursorLockMode.Locked;
        if(allPlanetListContainer)
            allPlanetListContainer.SetActive(true);
        if(detailContainer)
            detailContainer.SetActive(false);
        closeCam.Priority = 0;
        target.SetActive(PovController.getIsInDynamicMode());
    }
}
