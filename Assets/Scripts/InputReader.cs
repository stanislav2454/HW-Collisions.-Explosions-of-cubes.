using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action MouseButtonDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseButtonDown.Invoke();
    }
}
