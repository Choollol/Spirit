using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleComponent : MonoBehaviour, IMeleeInteractable, IRangedInteractable
{
    protected new Collider2D collider;

    [SerializeField] protected int parentDepth;
    //[SerializeField] protected GameObjectMessenger currentPuzzleMessenger;

    protected Transform controllerTransform;
    protected bool isCompleted = false;

    protected bool isMeleeInteractable;
    protected bool isRangedInteractable;

    protected bool doStopInteraction = true;

    protected bool canInteract = true;

    public virtual void Awake()
    {
        controllerTransform = transform.parent;
        for (int i = 0; i < parentDepth; i++)
        {
            controllerTransform = controllerTransform.parent;
        }

        collider = GetComponent<Collider2D>();
    }
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("Reset" + controllerTransform.name, ResetPuzzle);
        EventMessenger.StartListening("CompleteComponents" + controllerTransform.name, Complete);
        EventMessenger.StartListening("SetComplete" + controllerTransform.name, SetComplete);
        EventMessenger.StartListening("ResetMessenger" + controllerTransform.name, ResetMessenger);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("Reset" + controllerTransform.name, ResetPuzzle);
        EventMessenger.StopListening("CompleteComponents" + controllerTransform.name, Complete);
        EventMessenger.StopListening("SetComplete" + controllerTransform.name, SetComplete);
        EventMessenger.StopListening("ResetMessenger" + controllerTransform.name, ResetMessenger);
    }
    protected virtual void SetComplete()
    {
        Complete();
    }
    public virtual void Complete()
    {
        isCompleted = true;
        if (collider)
        {
            collider.enabled = false;
        }
    }
    protected virtual void ResetMessenger()
    {

    }
    public virtual void ResetPuzzle()
    {

    }
    public virtual void MeleeInteract()
    {
        if (!isMeleeInteractable)
        {
            return;
        }
        SetCurrentPuzzle();
        if (doStopInteraction)
        {
            EventMessenger.TriggerEvent("MeleeInteracted");
        }
    }
    public virtual void RangedInteract()
    {
        if (!isRangedInteractable)
        {
            return;
        }
        SetCurrentPuzzle();
        if (doStopInteraction)
        {
            EventMessenger.TriggerEvent("RangedInteracted");
        }
    }
    protected void SetCurrentPuzzle()
    {
        if (ObjectMessenger.GetGameObject("currentPuzzle") == controllerTransform.gameObject)//currentPuzzleMessenger.objects[0] == controllerTransform.gameObject)
        {
            return;
        }
        //currentPuzzleMessenger.objects[1] = controllerTransform.gameObject;
        ObjectMessenger.SetGameObject("newPuzzle", controllerTransform.gameObject);
        EventMessenger.TriggerEvent("SetCurrentPuzzle");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Interacter"))
        {
            MeleeInteract();
        }
        if (collision.gameObject.CompareTag("Ranged Interacter"))
        {
            RangedInteract();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Interacter"))
        {
            MeleeInteract();
        }
        if (collision.gameObject.CompareTag("Ranged Interacter"))
        {
            RangedInteract();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Interacter") && isMeleeInteractable)
        {
            if (doStopInteraction)
            {
                EventMessenger.TriggerEvent("MeleeInteracted");
            }
        }
    }
}
