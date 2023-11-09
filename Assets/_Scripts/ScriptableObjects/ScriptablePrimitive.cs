using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptablePrimitive", menuName = "ScriptableObjects/ScriptablePrimitive")]
public class ScriptablePrimitive : ScriptableObject
{
    public List<float> floats;
    public List<int> ints;
    public List<bool> bools;
    public List<string> strings;
}
