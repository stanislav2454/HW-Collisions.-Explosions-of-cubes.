using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private CubeRaycaster _raycaster;

    private Vector3 _cubePosition;

    private const float ChanceReductionFactor = 0.5f;

    private void OnEnable() =>
        _raycaster.OnCubeHit += OnCubeHit;

    private void OnDisable() =>
        _raycaster.OnCubeHit -= OnCubeHit;

    private void OnCubeHit(GameObject hitCube)
    {
        if (hitCube.TryGetComponent<Cube>(out var cubeData))
        {
            if (Random.value <= cubeData.SplitChance)
            {
                if (TryGetComponent(out CubeExploder exploder))
                {
                    var newCubes = CreateCubes(
                        hitCube.transform.position,
                        cubeData.SplitChance * ChanceReductionFactor,
                        hitCube.transform.localScale);

                    exploder.Explode(newCubes);
                }
            }
        }

        Destroy(hitCube.gameObject);
    }

    private GameObject[] CreateCubes(Vector3 position, float newSplitChance, Vector3 originalScale)
    {
        const int MinCubesCount = 2;
        const int MaxCubesCount = 6;
        const float InstanceRadius = 1.5f;

        int newCubesCount = Random.Range(MinCubesCount, MaxCubesCount + 1);

        GameObject[] newCubes = new GameObject[newCubesCount];

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * InstanceRadius;
            randomOffset.y = Mathf.Abs(randomOffset.y);
            GameObject newCube = Instantiate(_cube, position + Vector3.up + randomOffset, Quaternion.identity);
            newCube.transform.localScale = originalScale * newSplitChance;
            if (newCube.TryGetComponent<Cube>(out var newCubeData))
            {
                newCubeData.SetSplitParameters(newSplitChance);
            }
            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            newCubes[i] = newCube;
        }

        return newCubes;
    }
}