using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMessenger : MonoBehaviour
{
    private static Dictionary<string, GameObject> gameObjects;

    private void Awake()
    {
        gameObjects = new Dictionary<string, GameObject>();
    }
    public static GameObject GetGameObject(string key)
    {
        if (gameObjects.TryGetValue(key, out GameObject obj))
        {
            return obj;
        }
        else
        {
            return null;
        }
    }
    public static void SetGameObject(string key, GameObject obj)
    {
        if (gameObjects.TryGetValue(key, out _))
        {
            gameObjects[key] = obj;
        }
        else
        {
            gameObjects.Add(key, obj);
        }
    }
}
