using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrimitiveMessenger : MonoBehaviour
{
    public static Dictionary<string, float> floats;
    public static Dictionary<string, string> strings;
    public static Dictionary<string, bool> bools;

    private void Awake()
    {
        floats = new Dictionary<string, float>();
        strings = new Dictionary<string, string>();
        bools = new Dictionary<string, bool>();
    }
}
