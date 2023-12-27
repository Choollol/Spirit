using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IMeleeInteractable
{
    [SerializeField] private GameObject collectedParticle;

    [SerializeField] protected float expToGive;

    private bool isCompleted = false;
    private void OnEnable()
    {
        EventMessenger.StartListening("SetComplete" + name, Complete);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("SetComplete" + name, Complete);
    }   
    public void MeleeInteract()
    {
        EventMessenger.TriggerEvent("MeleeInteracted");
        if (isCompleted)
        {
            return;
        }
        Complete();
        PuzzleManager.CompletePuzzle(gameObject.scene.name, name);
        Instantiate(collectedParticle, transform.position, Quaternion.identity);
        AudioPlayer.PlaySound("Collectible Collected Sound");
        PrimitiveMessenger.floats["expToGive"] = expToGive;
        EventMessenger.TriggerEvent("PuzzleCompleted");
    }
    private void Complete()
    {
        isCompleted = true;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Interacter"))
        {
            MeleeInteract();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melee Interacter"))
        {
            EventMessenger.TriggerEvent("MeleeInteracted");
        }
    }
}
