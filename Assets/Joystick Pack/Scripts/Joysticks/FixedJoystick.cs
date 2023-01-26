using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        handle.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        handle.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        handle.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}