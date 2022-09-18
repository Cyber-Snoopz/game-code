using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PovController : MonoBehaviour
{
    [SerializeField] GameObject listAllPlanetContainer;
    [SerializeField] CinemachineVirtualCamera mainDynamicCam;
    [SerializeField] GameObject target;
    private static bool isInDynamicMode = false;

    void Start()
    {
        if(!mainDynamicCam || !listAllPlanetContainer)
            Debug.LogError("Camera not assigned properly!");
    }

    public void toggleDynamicMode()
    {
        if(isInDynamicMode)
        {
            mainDynamicCam.Priority = 1;
            isInDynamicMode = false;
        }
        else{
            mainDynamicCam.Priority = 4;
            isInDynamicMode = true;
        }
        if(target)
            target.SetActive(isInDynamicMode);
        listAllPlanetContainer.SetActive(false);
    }

    public static bool getIsInDynamicMode()
    {
        return isInDynamicMode;
    }
}
