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
            return;// без этой проверки при клике на любой куб, событие срабатывает на все кубы на сцене...

        Destroy(gameObject);

        if (Random.value <= _currentSplitChance)
        {
            Debug.LogWarning(_currentSplitChance);
            CreateCubes();
        }
    }

    private void CreateCubes()
    {
        const int MinCubesCount = 2;
        const int MaxCubesCount = 6;
        const float Divider = 2f;
        const float InstanceRadius = 1.5f;

        int newCubesCount = Random.Range(MinCubesCount, MaxCubesCount + 1);
        float newSplitChance = _currentSplitChance * ChanceReductionFactor;

        GameObject[] newCubes = new GameObject[newCubesCount];

        for (int i = 0; i < newCubes.Length; i++)
        {
            GameObject newCube = Instantiate(_cube);
            CubeSpawner spawner = newCube.GetComponent<CubeSpawner>();
            spawner.SetSplitChance(newSplitChance);
            newCube.transform.localScale = transform.localScale / Divider;
            newCube.transform.position = transform.position + new Vector3(0, 1, 0) + Random.insideUnitSphere * InstanceRadius;
            newCube.GetComponent<CubeSpawner>().OnEnable();// не понимаю почему, скрипт CubeSpawner на новых кубах выключен... 
            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

            //  newCubes[i] = newCube;
        }
    }

    public void SetSplitChance(float chance)
    {
        _currentSplitChance = chance;
    }
}