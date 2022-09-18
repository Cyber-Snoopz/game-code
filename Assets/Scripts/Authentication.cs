using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Authentication : MonoBehaviour
{
    bool isAuthenticated;
    public static Authentication instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        isAuthenticated = false;
    }

    public void OnAuthenticated()
    {
        isAuthenticated = true;
    }

    public void OnDisconnected()
    {
        isAuthenticated = false;
        Application.Quit();
    }

    public bool getIsAuthenticated()
    {
        return isAuthenticated;
    }
}
