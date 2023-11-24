using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectMessenger", menuName = "ScriptableObjects/GameObjectMessenger")]
public class GameObjectMessenger : ScriptableObject
{
    public List<GameObject> objects = new List<GameObject>();
}
