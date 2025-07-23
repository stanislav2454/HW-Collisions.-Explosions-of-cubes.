using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public event Action MouseButtonDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseButtonDown.Invoke();
    }
}
