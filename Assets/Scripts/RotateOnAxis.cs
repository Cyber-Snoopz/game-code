using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] bool xAxis = false;
    [SerializeField] bool yAxis = true;
    [SerializeField] bool zAxis = false;
    void Update()
    {
        if(yAxis)
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        if(xAxis)
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        if(zAxis)
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
