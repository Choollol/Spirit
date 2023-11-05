using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public static bool isGameActive { get; private set; }
    private static bool isMenuOpen;
    private static bool isInTransition;
    private static bool isInWorld;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        isGameActive = true;
    }

    void Update()
    {
        if (InputManager.GetButtonDown("Cancel"))
        {
            if (isMenuOpen)
            {
                EventMessenger.TriggerEvent("CloseMenu");
                isMenuOpen = false;
            }
            if (isGameActive)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }
    private static void PauseGame()
    {
        isGameActive = false;
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync("Pause_Menu", LoadSceneMode.Additive);
        isMenuOpen = true;
    }
    private static void UnpauseGame()
    {
        isGameActive = true;
        Time.timeScale = 1;
    }
    public static void OtherMenuOpened()
    {
        isMenuOpen = true;
        if (isInWorld)
        {
            EventMessenger.TriggerEvent("SetPlayerCanActFalse");
        }
    }
    public static void OtherMenuClosed()
    {
        isMenuOpen = false;
        if (isInWorld)
        {
            EventMessenger.TriggerEvent("SetPlayerCanActTrue");
        }
    }
}
