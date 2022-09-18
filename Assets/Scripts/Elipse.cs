using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Elipse 
{
    public float xAxis = 5f;
    public float yAxis = 2f;

    public Elipse(float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector2 Process(float t)
    {
        float angle = Mathf.Deg2Rad * t * 360;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
}
