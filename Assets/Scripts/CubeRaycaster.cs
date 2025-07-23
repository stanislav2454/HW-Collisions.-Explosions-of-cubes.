using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 40f;

    private Color hitColor = Color.red;
    private Camera _mainCamera;
    private UserInput _userInput;

    public event System.Action<CubeSpawner> OnCubeHit;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _userInput = FindObjectOfType<UserInput>();
    }

    private void OnEnable() =>
        _userInput.MouseButtonDown += PerformRaycast;

    private void OnDisable() =>
        _userInput.MouseButtonDown -= PerformRaycast;


    public void PerformRaycast()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            if (hit.transform == null) 
                return;

            if (hit.transform.GetComponent<CubeSpawner>() == null) 
                return;

            OnCubeHit?.Invoke(hit.transform.GetComponent<CubeSpawner>());
            Debug.Log($"Hit: {hit.collider.name}");
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, hitColor, 2f);
        }
    }
}