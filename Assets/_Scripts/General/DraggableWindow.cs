using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool doFollowMouse;
    private Vector2 offset;
    private void Update()
    {
        if (doFollowMouse)
        {
            transform.position = (Vector2)Input.mousePosition - offset;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        {
            offset = Input.mousePosition - transform.position;
            doFollowMouse = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        doFollowMouse = false;
    }
}
