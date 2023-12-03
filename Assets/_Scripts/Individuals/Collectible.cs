using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IMeleeInteractable
{
    private bool isCompleted = false;
    private void Complete()
    {
        isCompleted = true;
        gameObject.SetActive(false);
    }
    public void MeleeInteract()
    {
        if (isCompleted)
        {
            return;
        }
        Complete();
        //AudioPlayer.PlaySound("Collectible Collected Sound");
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
