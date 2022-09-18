using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loader;
    [SerializeField] Animator cutCanvas;
    [SerializeField] GameObject gamePlayCanvas;
    public static SceneLoader instance;

    /*
        Basic Singleton pattern
    */
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        if(cutCanvas)
            cutCanvas.SetTrigger("FadeOut");
    }

    /*Handles all posibilities of loading Scene*/

    public void LoadSceneBYName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextSceneWithCut()
    {
        if (gamePlayCanvas)
            gamePlayCanvas.SetActive(false);
        if (cutCanvas)
            cutCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadNextSceneAsynchronous());
    }
    public void LoadNamedSceneWithCut(string name)
    {
        if (gamePlayCanvas)
            gamePlayCanvas.SetActive(false);
        if (cutCanvas)
            cutCanvas.gameObject.SetActive(true);
        StartCoroutine(LoadNamedSceneAsynchronous(name));
    }
    IEnumerator LoadNextSceneAsynchronous()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        float timeCount = 0f;
        if (loader)
        {
            loader.gameObject.SetActive(true);
            while (!asyncLoad.isDone && timeCount <= 3f)
            {
                timeCount += Time.deltaTime;
                int loadDot = (int)timeCount % 3;
                if (loadDot == 0)
                    loader.text = "Loading.";
                if (loadDot == 1)
                    loader.text = "Loading..";
                if (loadDot == 2)
                    loader.text = "Loading...";
                yield return null;
            }
        }
        if (cutCanvas)
        {
            cutCanvas.GetComponent<Animator>().SetTrigger("FadeIn");
            yield return new WaitForSeconds(1.5f);
        }
        asyncLoad.allowSceneActivation = true;
    }
    IEnumerator LoadNamedSceneAsynchronous(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        asyncLoad.allowSceneActivation = false;
        float timeCount = 0f;
        if (loader)
        {
            loader.gameObject.SetActive(true);
            while (!asyncLoad.isDone && timeCount <= 3f)
            {
                timeCount += Time.deltaTime;
                int loadDot = (int)timeCount % 3;
                if (loadDot == 0)
                    loader.text = "Loading.";
                if (loadDot == 1)
                    loader.text = "Loading..";
                if (loadDot == 2)
                    loader.text = "Loading...";
                yield return null;
            }
        }
        if (cutCanvas)
        {
            cutCanvas.GetComponent<Animator>().SetTrigger("FadeIn");
            yield return new WaitForSeconds(1.5f);
        }
        asyncLoad.allowSceneActivation = true;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
