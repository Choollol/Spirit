using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformData
{
    public TransformData(Transform transform)
    {
        position = new float[] { transform.position.x, transform.position.y, transform.position.z };
        rotation = new float[] { transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z };
        scale = new float[] { transform.localScale.x, transform.localScale.y, transform.localScale.z };
    }
    public float[] position = new float[3];
    public float[] rotation = new float[3];
    public float[] scale = new float[3];
}
