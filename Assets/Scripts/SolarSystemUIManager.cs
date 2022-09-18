using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SolarSystemUIManager : MonoBehaviour
{
    [SerializeField] GameObject planetListContainer;
    [SerializeField] GameObject informationContainer;
    [SerializeField] Image tickMark;

    int shouldItBeShownLater;
    private void Start()
    {
        if(PlayerPrefs.HasKey("shouldItBeShownLater"))
        {
            shouldItBeShownLater = PlayerPrefs.GetInt("shouldItBeShownLater");
            informationContainer.SetActive(shouldItBeShownLater == 1);
        }
        else    
            shouldItBeShownLater = 0;
        
        if(planetListContainer)
        {
            planetListContainer.SetActive(false);
        }
    }
    public void TogglePlanetList()
    {
        if(planetListContainer)
        {
            planetListContainer.SetActive(!planetListContainer.activeSelf);
        }
    }

    public void onTickRecieved()
    {
        if(shouldItBeShownLater == 0)
        {   
            shouldItBeShownLater = 1;
            tickMark.color = new Color(0, 0, 0, 100);
            PlayerPrefs.SetInt("shouldItBeShownLater", shouldItBeShownLater);
            return;
        }
        else
        {
            shouldItBeShownLater = 0;
            tickMark.color = new Color(255, 255, 255, 75);
            PlayerPrefs.SetInt("shouldItBeShownLater", shouldItBeShownLater);
            return;
        }
    }
     public void onContinuePressed()
     {
        informationContainer.SetActive(false);
     }
}
