using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerTop : MonoBehaviour
{
    private void OnEnable()
    {
        EventMessenger.StartListening("SetComplete" + transform.parent.parent.name, SetComplete);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("SetComplete" + transform.parent.parent.name, SetComplete);
    }
    private void SetComplete()
    {
        transform.SetLocalPosY(FallinkTowerHolder.spacing / 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            EventMessenger.TriggerEvent("Check" + transform.parent.parent.name);
        }
    }
}
