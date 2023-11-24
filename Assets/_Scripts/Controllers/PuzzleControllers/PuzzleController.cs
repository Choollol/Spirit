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

    [SerializeField] protected ScriptablePrimitive messenger;
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("Reset", ResetPuzzle);
        EventMessenger.StartListening("Check" + name, CheckForCompletion);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("Reset", ResetPuzzle);
        EventMessenger.StopListening("Check" + name, CheckForCompletion);
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
    }
    public virtual void SetComplete()
    {
        isCompleted = true;
        EventMessenger.TriggerEvent("Complete" + name);
    }
    protected virtual void DeactivatePuzzle()
    {
        if (GameManager.currentPuzzle != gameObject)
        {
            return;
        }
        GameManager.currentPuzzle = null;
    }
    protected virtual void SetCurrentPuzzle()
    {
        if (GameManager.currentPuzzle == gameObject)
        {
            return;
        }
        GameManager.currentPuzzle = gameObject;
        ResetMessenger();
    }
    protected virtual void CheckForCompletion()
    {
        if (GameManager.currentPuzzle != gameObject || isCompleted)
        {
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!messenger.bools[i])
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
        EventMessenger.TriggerEvent("Complete" + name);
        EventMessenger.TriggerEvent("PuzzleCompleted");
    }
    protected virtual void ResetMessenger()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            messenger.bools[i] = false;
        }
    }
    protected virtual void ResetPuzzle()
    {
        if (GameManager.currentPuzzle != gameObject || isCompleted)
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
            SetCurrentPuzzle();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeactivatePuzzle();
        }
    }
}
