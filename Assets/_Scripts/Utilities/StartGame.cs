using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameStart());
        //SceneManager.LoadSceneAsync("Test", LoadSceneMode.Additive);
    }
    private IEnumerator GameStart()
    {
        AsyncOperation loadMain = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        while (loadMain.progress < 0.9f)
        {
            yield return null;
        }
        loadMain.allowSceneActivation = false;
        AsyncOperation loadOther = SceneManager.LoadSceneAsync("Test", LoadSceneMode.Additive);
        while (loadOther.progress < 0.9f)
        {
            yield return null;
        }
        loadMain.allowSceneActivation = true;
        while (!loadMain.isDone || !loadOther.isDone)
        {
            yield return null;
        }
        EventMessenger.TriggerEvent("GameLoaded");
        SceneManager.UnloadSceneAsync("Start_Game");
        yield break;
    }
}
