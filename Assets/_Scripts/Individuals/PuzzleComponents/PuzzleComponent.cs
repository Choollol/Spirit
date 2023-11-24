using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleComponent : MonoBehaviour, IInteractable
{
    [SerializeField] protected int parentDepth;
    protected Transform controllerTransform;
    protected bool isCompleted = false;
    public virtual void Awake()
    {
        controllerTransform = transform.parent;
        for (int i = 0; i < parentDepth; i++)
        {
            controllerTransform = controllerTransform.parent;
        }
    }
    public virtual void OnEnable()
    {
        EventMessenger.StartListening("Reset" + controllerTransform.name, ResetPuzzle);
        EventMessenger.StartListening("Complete" + controllerTransform.name, Complete);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("Reset" + controllerTransform.name, ResetPuzzle);
        EventMessenger.StopListening("Complete" + controllerTransform.name, Complete);
    }
    public virtual void Complete()
    {
        isCompleted = true;
    }
    public virtual void ResetPuzzle()
    {

    }
    public virtual void MeleeInteract()
    {

    }
    public virtual void RangedInteract()
    {

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
}
