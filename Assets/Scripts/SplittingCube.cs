using UnityEngine;

public class SplittingCube : MonoBehaviour
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject _cube;

    private float currentSplitChance = 1f;
    private const float ChanceReductionFactor = 0.5f;
    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        if (Random.value <= currentSplitChance)
        {
            SplitCube();
            currentSplitChance *= ChanceReductionFactor;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SplitCube()
    {
        const int MinCubesCount = 2;
        const int MaxCubesCount = 6;

        int newCubesCount = Random.Range(MinCubesCount, MaxCubesCount + 1);
        GameObject[] newCubes = new GameObject[newCubesCount];
        Vector3 originalPosition = transform.position;

        CreateCubes(newCubesCount, newCubes, originalPosition);
        ExplodeObjects(newCubes, originalPosition);
        Destroy(gameObject);
    }

    private void CreateCubes(int newCubesCount, GameObject[] newCubes, Vector3 originalPosition)
    {
        const float Divider = 2f;
        const float InstanceRadius = 1.5f;

        Vector3 originalScale = transform.localScale;

        for (int i = 0; i < newCubesCount; i++)
        {
            GameObject newCube = Instantiate(_cube);
            newCube.transform.localScale = originalScale / Divider;
            newCube.transform.position = originalPosition + new Vector3(0, 1, 0) + Random.insideUnitSphere * InstanceRadius;

            newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

            newCubes[i] = newCube;
        }
    }

    private void ExplodeObjects(GameObject[] newCubes, Vector3 originalPosition)
    {
        foreach (GameObject cube in newCubes)
        {
            Rigidbody cubeRb = cube.GetComponent<Rigidbody>();
            cubeRb.AddExplosionForce(explosionForce, originalPosition, explosionRadius);
        }
    }
}