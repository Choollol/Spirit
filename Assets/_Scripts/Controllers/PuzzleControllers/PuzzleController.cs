using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    protected List<Transform> transforms = new List<Transform>();
    protected List<TransformData> transformData = new List<TransformData>();
    protected List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    public bool isCompleted;

    [SerializeField] protected bool doResetOnCheckFail;

    protected int boolsToCheck;

    [SerializeField] protected float expToGive;

    //[SerializeField] protected GameObjectMessenger currentPuzzleMessenger;
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("Reset", ResetPuzzle);
        EventMessenger.StartListening("Check" + name, CheckForCompletion);
        //EventMessenger.StartListening("CurrentPuzzleChanged", SetCurrentPuzzle);
        EventMessenger.StartListening("SetComplete" + name, SetComplete);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("Reset", ResetPuzzle);
        EventMessenger.StopListening("Check" + name, CheckForCompletion);
        //EventMessenger.StopListening("CurrentPuzzleChanged", SetCurrentPuzzle);
        EventMessenger.StopListening("SetComplete" + name, SetComplete);
    }
    public virtual void Start()
    {
        foreach (Transform t in transform.GetChildTransforms())
        {
            transforms.Add(t);
            transformData.Add(new TransformData(t));
            if (t.GetComponent<Rigidbody2D>() != null)
            {
                rigidbodies.Add(t.GetComponent<Rigidbody2D>());
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            PrimitiveMessenger.bools[name + i] = false;
        }
        boolsToCheck = transform.childCount;
        ResetMessenger();
    }
    public virtual void SetComplete()
    {
        Complete();
        EventMessenger.TriggerEvent("SetComplete" + name);
    }
    protected virtual void DeactivatePuzzle()
    {
        if (ObjectMessenger.GetGameObject("currentPuzzle") != gameObject)//currentPuzzleMessenger.objects[0] != gameObject)
        {
            return;
        }
        ObjectMessenger.SetGameObject("currentPuzzle", null);
    }
    /*protected virtual void SetCurrentPuzzle()
    {
        //ResetMessenger();
    }*/
    protected virtual void CheckForCompletion()
    {
        if (ObjectMessenger.GetGameObject("currentPuzzle") != gameObject || isCompleted)
        {
            return;
        }
        for (int i = 0; i < boolsToCheck; i++)
        {
            if (!PrimitiveMessenger.bools[name + i])
            {
                if (doResetOnCheckFail)
                {
                    ResetPuzzle();
                }
                return;
            }
        }
        Complete();
        PrimitiveMessenger.floats["expToGive"] = expToGive;
        EventMessenger.TriggerEvent("PuzzleCompleted");
        EventMessenger.TriggerEvent("CompleteComponents" + name);
        AudioPlayer.PlaySound("Complete Sound");
    }
    protected virtual void Complete()
    {
        if (isCompleted)
        {
            return;
        }
        isCompleted = true;
        OnDisable();
        PuzzleManager.CompletePuzzle(gameObject.scene.name, name);
    }
    protected virtual void ResetMessenger()
    {
        for (int i = 0; i < boolsToCheck; i++)
        {
            PrimitiveMessenger.bools[name + i] = false;
        }
        EventMessenger.TriggerEvent("ResetMessenger" + name);
    }
    protected virtual void ResetPuzzle()
    {
        if (ObjectMessenger.GetGameObject("currentPuzzle") != gameObject || isCompleted)
        {
            return;
        }
        for (int i = 0; i < transforms.Count; i++)
        {
            transforms[i].gameObject.SetActive(true);
            transforms[i].TransferTransformData(transformData[i]);
            if (i < rigidbodies.Count)
            {
                rigidbodies[i].velocity = Vector2.zero;
                rigidbodies[i].angularVelocity = 0;
            }
        }
        ResetMessenger();
        EventMessenger.TriggerEvent("Reset" + transform.name);
        AudioPlayer.PlaySound("Reset Sound");
    }
}
