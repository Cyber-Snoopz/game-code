using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeMovement : MonoBehaviour
{
    [Header("Camera Movement Params")]
    [SerializeField] float movementMultiplier = 5f;
    [SerializeField] float mouseSensitivity = 5f;
    float rotationX;
    float rotationY;
    bool isInDynamicMode = false;
    float initialFieldOfView;
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 70f;
        initialFieldOfView = GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
    }
    void Update()
    {
        isInDynamicMode = PovController.getIsInDynamicMode();
        if(Input.GetKey(KeyCode.LeftAlt) || !isInDynamicMode)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            // if(Authentication.instance.getIsAuthenticated())
            processInput();
        }
    }

    private void processInput()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!SelectObject.isSelected)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseMovement();
            movement();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void mouseMovement()
    {
        
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity * Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y")*mouseSensitivity * Time.deltaTime;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  
        rotationY +=  mouseX;
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    private void movement()
    {
        float zDir = Input.GetAxis("Vertical")*movementMultiplier * Time.deltaTime;
        float xDir = Input.GetAxis("Horizontal")*movementMultiplier * Time.deltaTime;
        if(Mathf.Abs(zDir) > Mathf.Epsilon || Mathf.Abs(xDir) > Mathf.Epsilon)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView, 
            90f, Time.deltaTime*1f);
        }
        else{
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView,
            initialFieldOfView ,Time.deltaTime*1f);
        }

        Vector3 direction = new Vector3((Vector3.right * xDir).x, 0f, (Vector3.forward * zDir).z);
        transform.Translate(direction);
    }
}
