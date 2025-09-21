using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event System.Action MouseButtonDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseButtonDown?.Invoke();
    }
}