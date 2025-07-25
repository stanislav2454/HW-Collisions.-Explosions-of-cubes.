using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _maxExplosionForce = 9f;

    private float _explosionRadius = 8f;

    private float _upwardsModifier = 1f;
    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode(Cube[] cubes)
    {
        foreach (var cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(
           _maxExplosionForce,
           cube.transform.position,
           _explosionRadius,
           _upwardsModifier,
           _forceMode);
        }
    }

    public void Explode(List<Rigidbody> cubes, Vector3 expCenter, float explosionRadius)
    {
        foreach (var cube in cubes)
        {
            cube.AddExplosionForce(
                CalculateExplosionForce(cube, expCenter),
                expCenter,
                explosionRadius);
        }
    }

    private float CalculateExplosionForce(Rigidbody cube, Vector3 expCenter)
    {
        float distance = Vector3.Distance(expCenter, cube.position);
        Debug.Log(distance);
        if (distance < 0.01f)
            return _maxExplosionForce;

        return _maxExplosionForce / distance;
    }
}