using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(OrbitMotion))]
public class ElipseRenderer : MonoBehaviour
{   
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] [Range(50, 200)] int segments = 50;
    [SerializeField] OrbitMotion orbitMotion;
    [SerializeField] Elipse elipse;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments+1;
        
    }
    void Start()
    {
        orbitMotion = GetComponent<OrbitMotion>();
        elipse = orbitMotion.getGeneratedElipse();
    }
    void Update()
    {
        elipse = orbitMotion.getGeneratedElipse();
        CalculateElipse();
    }
    public void CalculateElipse()
    {
        Vector3[] points = new Vector3[segments+1];
        for(int i = 0; i < segments; i++)
        {
            Vector2 position2D = elipse.Process((float)i/(float)segments);
            points[i] = new Vector3(position2D.x, 0f, position2D.y);
        }
        points[segments] = points[0];
        lineRenderer.SetPositions(points);
    }
}
