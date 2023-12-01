using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public virtual void Awake()
    {
        GameManager.OtherMenuOpened();
    }
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("CloseMenu", CloseScene);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("CloseMenu", CloseScene);
    }
    protected void CloseScene()
    {
        GameManager.OtherMenuClosed();
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
