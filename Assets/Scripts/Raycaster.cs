using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 40f;
    [SerializeField] private InputReader _userInput;

    private Color hitColor = Color.red;
    private Camera _mainCamera;

    public event System.Action<ExplodingCube> CubeHitted;

    private void Awake() =>
       _mainCamera = Camera.main;

    private void OnEnable() =>
        _userInput.MouseButtonDown += PerformRaycast;

    private void OnDisable() =>
        _userInput.MouseButtonDown -= PerformRaycast;

    public void PerformRaycast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
            if (hit.transform.GetComponent<ExplodingCube>())
            {
                CubeHitted?.Invoke(hit.collider.GetComponent<ExplodingCube>());
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, hitColor, 2f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue, 2f);
            }
        }
    }
}