using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoralisUnity;
public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] TextMeshProUGUI addressText;
    
    [SerializeField] GameObject authCanvas;

    private bool isAuthenticated;
    private bool prevIsNotAuthenticated;
    void Start()
    {
        if(!gameplayCanvas || !addressText || !authCanvas)
            Debug.Log("UiManager: Missing UI elements");

        isAuthenticated = false;
        prevIsNotAuthenticated = false;
        gameplayCanvas.SetActive(false);
        authCanvas.SetActive(true);
    }
    
    async void Update()
    {
        isAuthenticated = Authentication.instance.getIsAuthenticated();
        if(prevIsNotAuthenticated != isAuthenticated)
        {
            if(isAuthenticated)
            {
                var user = await Moralis.GetUserAsync();
                gameplayCanvas.SetActive(true);
                if(authCanvas)
                    authCanvas.SetActive(false);
                if(addressText)
                    addressText.text = user.ethAddress.ToString();
            }
        }
    }
}
