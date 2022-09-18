using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject gameContainer;
    [SerializeField] GameObject menuContainer;

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
    public void OnClickBack()
    {
        gameContainer.SetActive(false);
        menuContainer.SetActive(true);
    }
}
