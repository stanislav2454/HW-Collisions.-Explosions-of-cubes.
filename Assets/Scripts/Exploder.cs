using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _maxExplosionForce = 9f;
    private float _minExplosionForce = 0.1f;
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
            Debug.Log($"<color=green>{cube.name}</color> - damaged !");
            cube.AddExplosionForce(
                CalculateExplosionForce(cube, expCenter),
                expCenter,
                explosionRadius);
        }
    }

    private float CalculateExplosionForce(Rigidbody cube, Vector3 expCenter)
    {
        float distance = Vector3.Distance(expCenter, cube.position);
        if (distance < 0.01f)
            return _maxExplosionForce;

        Debug.Log($"<color=red>Distance - </color>{distance}");
        float force = _maxExplosionForce / distance;
        if (force < 0.1f)
            force = 0;

        Debug.Log($"<color=blue>ExplosionForce - </color>{force}");
        return force;
    }
}