using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeTest : MonoBehaviour
{
    public bool IsTriggered;

    public void OnScale(InputAction.CallbackContext context)
    {
        // IsTriggered = context.ReadValue<bool>();
        IsTriggered = context.action.triggered;
    }

    private void Update()
    {
        if (IsTriggered)
        {
            print("hello");
            transform.localScale += Vector3.one;
        }
    }
}
