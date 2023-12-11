using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum World
    {
        Forest, Test2
    }

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public bool IsGameActive;
    public static bool isGameActive { get; private set; } // Whether game is paused or not
    private static bool isMenuOpen;
    public static bool doCloseMenuOnCancel = true;
    private static bool isInTransition;
    private static bool isInWorld;

    public static World currentWorld { get; private set; }
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
    private void OnEnable()
    {
        EventMessenger.StartListening("ExitGame", ExitGame);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("ExitGame", ExitGame);
    }
    void Start()
    {
        isGameActive = IsGameActive;
    }

    void Update()
    {
        if (InputManager.GetButtonDown("Cancel"))
        {
            if (isMenuOpen && doCloseMenuOnCancel)
            {
                EventMessenger.TriggerEvent("CloseMenu");
                isMenuOpen = false;
                if (!isGameActive)
                {
                    UnpauseGame();
                }
            }
            else if (isGameActive && !isInTransition)
            {
                SceneManager.LoadSceneAsync("Pause_Menu", LoadSceneMode.Additive);
                isMenuOpen = true;
                PauseGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentWorld == World.Forest)
            {
                SwitchWorld("Test2");
            }
            else
            {
                SwitchWorld("Forest");
            }
        }
    }
    private void SwitchWorld(string newWorld)
    {
        StartCoroutine(HandleSwitchWorld(newWorld));
    }
    private IEnumerator HandleSwitchWorld(string newWorld)
    {
        EventMessenger.TriggerEvent("StartTransition");
        InputManager.allowInput = false;
        isInTransition = true;
        while (PrimitiveMessenger.bools["isTransitionFading"])
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(currentWorld.ToString());
        SceneManager.LoadSceneAsync(newWorld, LoadSceneMode.Additive);
        currentWorld = (World)Enum.Parse(typeof(World), newWorld);
        yield return new WaitForSeconds(0.5f);
        EventMessenger.TriggerEvent("WorldSwitched");
        yield return new WaitForSeconds(0.5f);
        EventMessenger.TriggerEvent("EndTransition");
        while (PrimitiveMessenger.bools["isTransitionFading"])
        {
            yield return null;
        }
        InputManager.allowInput = true;
        isInTransition = false;
        yield break;
    }
    private static void PauseGame()
    {
        isGameActive = false;
        Time.timeScale = 0;
        
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
    private static void ExitGame()
    {
        EventMessenger.TriggerEvent("Save");
        Application.Quit();
    }
}
