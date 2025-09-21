using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 40f;
    [SerializeField] private InputReader _userInput;

    private Color hitColor = Color.red;
    private Color _missColor = Color.blue;
    private Camera _mainCamera;

    public event System.Action<Cube> CubeHitted;

    private void Awake() =>
       _mainCamera = Camera.main;

    private void OnEnable() =>
        _userInput.MouseButtonDown += PerformRaycast;

    private void OnDisable() =>
        _userInput.MouseButtonDown -= PerformRaycast;

    public void PerformRaycast()
    {
        const float Duration = 3f;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
            if (hit.transform.TryGetComponent(out Cube cube))
            {
                CubeHitted?.Invoke(cube);//-> CubeSpawner-> void TrySplitOrDestroy (Cube hitCube)
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, hitColor, Duration);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, _missColor, Duration);
            }
        }
    }
}