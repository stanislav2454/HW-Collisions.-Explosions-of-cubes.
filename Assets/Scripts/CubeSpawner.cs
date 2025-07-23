using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private CubeRaycaster _raycaster;

    private const float ChanceReductionFactor = 0.5f;

    private float _currentSplitChance = 1f;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        _raycaster = FindObjectOfType<CubeRaycaster>();
    }

    private void OnEnable() =>
        _raycaster.OnCubeHit += TrySplitOrDestroy;

    private void OnDisable() =>
        _raycaster.OnCubeHit -= TrySplitOrDestroy;

    private void TrySplitOrDestroy(CubeSpawner hitCube)
    {
        if (hitCube != this)
            return;

        Destroy(gameObject);

        if (Random.value <= _currentSplitChance)
        {
            _currentSplitChance *= ChanceReductionFactor;
            CreateCubes();
            // ExplodeObjects(newCubes, originalPosition);
        }
        //else
        //{
        //Destroy(gameObject);
        //}
    }

    private void CreateCubes()
    {
        const int MinCubesCount = 2;
        const int MaxCubesCount = 6;
        const float Divider = 2f;
        const float InstanceRadius = 1.5f;

        int newCubesCount = Random.Range(MinCubesCount, MaxCubesCount + 1);
        GameObject[] newCubes = new GameObject[newCubesCount];

        Vector3 originalPosition = transform.position;
        Vector3 originalScale = transform.localScale;

        for (int i = 0; i < newCubes.Length; i++)
        {// Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
            GameObject newCube = Instantiate(_cube);
            // GameObject newCube = Instantiate(_cube, transform, true);
            newCube.transform.localScale = originalScale / Divider;
            newCube.transform.position = originalPosition + new Vector3(0, 1, 0) + Random.insideUnitSphere * InstanceRadius;
            newCube.GetComponent<CubeSpawner>().OnEnable();
            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

            newCubes[i] = newCube;
        }
    }
}