using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerTop : MonoBehaviour
{    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            EventMessenger.TriggerEvent("Check" + transform.parent.parent.name);
        }
    }
}
