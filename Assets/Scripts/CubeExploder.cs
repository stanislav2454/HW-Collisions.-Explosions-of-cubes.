using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 9f;
    [SerializeField] private float _explosionRadius = 8f;

    private float _upwardsModifier = 1f;
    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode(Cube[] cubes)
    {
        foreach (var cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(
           _explosionForce,
           cube.transform.position,
           _explosionRadius,
           _upwardsModifier,
           _forceMode);
        }
    }
}