using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    protected List<Transform> transforms = new List<Transform>();
    protected List<TransformData> transformData = new List<TransformData>();
    protected List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    public bool isCompleted;

    [SerializeField] protected bool doResetOnCompletionCheck;

    [SerializeField] protected GameObjectMessenger currentPuzzleMessenger;
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("Reset", ResetPuzzle);
        EventMessenger.StartListening("Check" + name, CheckForCompletion);
        EventMessenger.StartListening("CurrentPuzzleChanged", SetCurrentPuzzle);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("Reset", ResetPuzzle);
        EventMessenger.StopListening("Check" + name, CheckForCompletion);
        EventMessenger.StopListening("CurrentPuzzleChanged", SetCurrentPuzzle);
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
    }
    public virtual void SetComplete()
    {
        isCompleted = true;
        OnDisable();
        ResetMessenger();
        PuzzleManager.CompletePuzzle(name);
        EventMessenger.TriggerEvent("Complete" + name);
    }
    protected virtual void DeactivatePuzzle()
    {
        if (currentPuzzleMessenger.objects[0] != gameObject)
        {
            return;
        }
        currentPuzzleMessenger.objects[0] = null;
    }
    protected virtual void SetCurrentPuzzle()
    {
        ResetMessenger();
    }
    protected virtual void CheckForCompletion()
    {
        if (currentPuzzleMessenger.objects[0] != gameObject || isCompleted)
        {
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!PrimitiveMessenger.bools[name + i])//!messenger.bools[i])
            {
                if (doResetOnCompletionCheck)
                {
                    ResetPuzzle();
                }
                return;
            }
        }
        Complete();
    }
    protected virtual void Complete()
    {
        if (isCompleted)
        {
            return;
        }
        isCompleted = true;
        OnDisable();
        ResetMessenger();
        PuzzleManager.CompletePuzzle(name);
        EventMessenger.TriggerEvent("Complete" + name);
        EventMessenger.TriggerEvent("PuzzleCompleted");
        AudioPlayer.PlaySound("Complete Sound");
    }
    protected virtual void ResetMessenger()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //messenger.bools[i] = false;
            PrimitiveMessenger.bools[name + i] = false;
        }
    }
    protected virtual void ResetPuzzle()
    {
        if (currentPuzzleMessenger.objects[0] != gameObject || isCompleted)
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SetCurrentPuzzle();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //DeactivatePuzzle();
        }
    }
}
