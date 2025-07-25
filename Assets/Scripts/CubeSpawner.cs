using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private float _explosionRadius = 11f;

    private const float ChanceReductionFactor = 0.5f;

    private void OnEnable() =>
        _raycaster.CubeHitted += TrySplitOrDestroy;

    private void OnDisable() =>
        _raycaster.CubeHitted -= TrySplitOrDestroy;

    private void TrySplitOrDestroy(Cube hitCube)
    {
        if (TryGetComponent(out Exploder exploder))
        {
            if (Random.value <= hitCube.SplitChance)
            {// todo del "var newCubes ="
                CreateCubes(
                //var newCubes = CreateCubes(
                    hitCube.transform.position,
                    hitCube.SplitChance * ChanceReductionFactor,
                    hitCube.transform.localScale);
            }
            else
            {
                var explodedObjects = hitCube.GetExplodableObjects(_explosionRadius);
                exploder.Explode(explodedObjects, hitCube.transform.position, _explosionRadius);
            }
        }

        Destroy(hitCube.gameObject);
    }

    private Cube[] CreateCubes(Vector3 position, float newSplitChance, Vector3 originalScale)
    {
        const int MinCubesCount = 2;
        const int MaxCubesCount = 8;
        const float InstanceRadius = 1.5f;

        int newCubesCount = Random.Range(MinCubesCount, MaxCubesCount + 1);

        Cube[] newCubes = new Cube[newCubesCount];

        for (int i = 0; i < newCubesCount; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * InstanceRadius;
            randomOffset.y = Mathf.Abs(randomOffset.y);
            var newCube = Instantiate(_cube, position + Vector3.up + randomOffset, Quaternion.identity);
            newCube.transform.localScale = originalScale * newSplitChance;

            var cubeComponent = newCube.GetComponent<Cube>();
            cubeComponent.SetSplitParameters(newSplitChance);

            newCubes[i] = cubeComponent;
        }

        return newCubes;
    }
}