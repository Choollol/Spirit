using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IMeleeInteractable
{
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
        if (isCompleted)
        {
            return;
        }
        Complete();
        PuzzleManager.CompletePuzzle(gameObject.scene.name, name);
        //AudioPlayer.PlaySound("Collectible Collected Sound");
    }
    private void Complete()
    {
        isCompleted = true;
        gameObject.SetActive(false);
        PuzzleManager.CompletePuzzle(gameObject.scene.name, name);
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
