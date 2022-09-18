using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.EventSystems;
using Cinemachine;
public class SelectObject : MonoBehaviour
{
    Ray _ray;
    RaycastHit hit;
    Camera mainCam;
    [SerializeField] LayerMask layerMask;
    public static Planet selectedPlanet;
    public static bool isSelected = false;
    void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // if (Authentication.instance.getIsAuthenticated())
            _ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(_ray, out hit, Mathf.Infinity, layerMask))
            {
                Planet planet = hit.transform.GetComponent<Planet>();
                if(planet)
                {
                    CinemachineController.instance.focusOnObjectUsingUI(planet.getScriptableAssociated().id);
                }
            }
    }
}

