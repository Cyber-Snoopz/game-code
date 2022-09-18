using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    [SerializeField] Transform orbitingObject;
    [SerializeField] Elipse orbitPath;
    [SerializeField] Transform centerObject;

    [SerializeField] Light pointLight;
    [SerializeField] float pointLightDistance = 5f;

    [Range (0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;
    void Awake()
    {
        if(!centerObject)
        {
            centerObject = GameObject.FindGameObjectWithTag("Sun").transform;
        }
    }
    void Start()
    {
        orbitingObject = transform.GetChild(0);
        if(orbitingObject == null){
            orbitActive = false;
            return;
        }
        transform.position = centerObject.position;
        setOrbittingBodyPosition();
    }

    void keepLightOffset()
    {
        if(pointLight)
        {
            Vector3 dir = (centerObject.position - orbitingObject.position).normalized;
            pointLight.transform.position = orbitingObject.position + dir * pointLightDistance;
        }
    }
    void setOrbittingBodyPosition()
    {
        Vector2 orbitPosition = orbitPath.Process(orbitProgress);
        Vector3 newLocalPos =  new Vector3(orbitPosition.x, orbitingObject.localPosition.y, orbitPosition.y);
        orbitingObject.localPosition = newLocalPos;
    }

    void Update()
    {
        transform.position = centerObject.position;
        keepLightOffset();
    }
    void LateUpdate()
    {
        AnimateOrbit();
    }

    void AnimateOrbit()
    {
        if(Mathf.Abs(orbitPeriod) < 0.1f)
        {
            if(orbitPeriod < 0) orbitPeriod = -0.1f;
            else orbitPeriod = 0.1f;
        }
        float orbitSpeed = (2*Mathf.PI*orbitPath.xAxis)/orbitPeriod;
        if(orbitActive)
        {
            orbitProgress += Time.deltaTime*orbitSpeed;
            orbitProgress %= 1f;
            setOrbittingBodyPosition();
        }
    }

    public float getOrbitProgress()
    {
        return orbitProgress;
    }
    public Elipse getGeneratedElipse()
    {
        return orbitPath;
    }
    public Transform getOrbitingObject()
    {
        return orbitingObject;
    }
    public float getOrbitPeriod()
    {
        return orbitPeriod;
    }
    public void setCenterObject(Transform centerObject)
    {
        this.centerObject = centerObject;
        transform.position = centerObject.position;
    }
    public Transform getCenterObject()
    {
        return centerObject;
    }
}

