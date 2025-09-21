using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _maxExplosionForce = 9f;

    private float _explosionRadius = 8f;

    public void Explode(List<Rigidbody> cubes, Vector3 expCenter)
    {
        foreach (var cube in cubes)
        {
            float scale = cube.transform.localScale.x;
            float scaledExplosionRadius = _explosionRadius / scale;

            cube.AddExplosionForce(
                      CalculateExplosionForce(cube, expCenter, scale),
                      expCenter,
                      scaledExplosionRadius);
        }
    }

    //private float CalculateExplosionForce(Rigidbody cube, Vector3 expCenter, float scale)
    //{//Предыдущая версия, не оптимизированная
    //    float distance = Vector3.Distance(expCenter, cube.position);

    //    if (distance < 0.01f)
    //        return _maxExplosionForce / scale;

    //    return (_maxExplosionForce / scale) / distance;
    //}
    private float CalculateExplosionForce(Rigidbody cube, Vector3 expCenter, float scale)
    {//Это максимально производительный вариант для данной физической формулы.
        Vector3 direction = cube.position - expCenter;
        float sqrDistance = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;

        if (sqrDistance < 0.01f)
            return _maxExplosionForce / scale;

        float scaledForce = _maxExplosionForce / scale;
        return scaledForce / Mathf.Sqrt(sqrDistance);
    }
}