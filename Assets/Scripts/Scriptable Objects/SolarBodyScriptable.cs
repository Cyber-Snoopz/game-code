using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SolarBody", order = 1)]
public class SolarBodyScriptable : ScriptableObject
{
    public string celestialBodyName;
    public string radius;
    public string distanceFromSun;
    public string description;
    public string mass;
    public int numberOfMoons;
    public GameObject celestialBodyPrefab;
    public int id;
}
