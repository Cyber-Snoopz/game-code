using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour
{
    [SerializeField] Elipse elipse;
    [Range(50, 500)][SerializeField] int numberOfAsteroidInALayer = 200;
    [SerializeField] float verticalVariation = 10f;
    [SerializeField] GameObject[] asteroidPrefabs;
    [Range(0, 1)][SerializeField] float intialProgress = 0f;
    List<Vector3> spawnPositions;
    void Start()
    {
        spawnPositions = new List<Vector3>();
        for(int i = 0; i < numberOfAsteroidInALayer; i++)
        {
            float percentage = (((float)(i)/(float)numberOfAsteroidInALayer)+ +intialProgress)%1;
            Vector2 obtainedPos = elipse.Process(percentage);
            float yPos = Random.Range(-verticalVariation, verticalVariation);
            spawnPositions.Add(new Vector3(obtainedPos.x, yPos, obtainedPos.y));
        }
        if(asteroidPrefabs.Length == 0)
        {   
            Debug.Log("Add the asteroid prefabs");
            return;
        }
        
        //Instantiate the asteroid in spaw location
        foreach(Vector3 pos in spawnPositions)
        {
            int randomIndex = (int)Random.Range(0f, 10f)%asteroidPrefabs.Length;
            GameObject asteroid = Instantiate(asteroidPrefabs[randomIndex], pos, Quaternion.identity);
            asteroid.transform.parent = this.transform;
        }
    }
}
