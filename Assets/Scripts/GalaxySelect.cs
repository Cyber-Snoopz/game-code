using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GalaxySelect : MonoBehaviour
{
    [SerializeField] GameObject floatingContainer;
    [SerializeField] TextMeshProUGUI containerText;
    void Start()
    {
        if(!floatingContainer)
            Debug.Log("No container assigned");
        floatingContainer.SetActive(false);
    }

    void OnMouseOver()
    {
        floatingContainer.SetActive(true);
        if(gameObject.tag == "MilkyWay")
        {
            containerText.text = "Click To Enter The Galaxy!";
        }
        else{
            containerText.text = "Yet To Be Released!";
        }
    }

    void OnMouseExit()
    {
        floatingContainer.SetActive(false);

    }

    void OnMouseDown()
    {
        SceneLoader.instance.LoadNextSceneWithCut();
    }
}
