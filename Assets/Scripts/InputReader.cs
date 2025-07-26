using UnityEngine.Events;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event UnityAction MouseButtonDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseButtonDown?.Invoke();
    }
}
