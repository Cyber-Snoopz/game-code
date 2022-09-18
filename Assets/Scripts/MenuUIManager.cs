using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject gameContainer;
    [SerializeField] GameObject menuContainer;
    [SerializeField] GameObject creditsContainer;
    [SerializeField] GameObject helpContainer;
    void Awake()
    {
        if(!gameContainer || !menuContainer)    
            Debug.Log("No Containers assigned");
    }
    public void OnClickGame()
    {
        gameContainer.SetActive(true);
        menuContainer.SetActive(false);
    }
    public void OnClickCredits()
    {
        menuContainer.SetActive(false);
        creditsContainer.SetActive(true);
    }
    public void OnClickBack()
    {
        gameContainer.SetActive(false);
        menuContainer.SetActive(true);
        creditsContainer.SetActive(false);
        helpContainer.SetActive(false);
    }
    public void OnClickHelp()
    {
        helpContainer.SetActive(true);
        menuContainer.SetActive(false);
    }
}
