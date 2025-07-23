using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const float Delay = 0.03f;

    [SerializeField] private float _explosionForce = 333f;
    [SerializeField] private float _explosionRadius = 8f;

    private float _upwardsModifier = 1f;
    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode()
    {
        Vector3 explosionPosition = transform.position;
        var explodableObjects = GetExplodableObjects(explosionPosition, _explosionRadius);
        foreach (var rigidbody in explodableObjects)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddExplosionForce(
                _explosionForce,
                explosionPosition,
                _explosionRadius,
                _upwardsModifier,
                _forceMode);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 pos, float explosionRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);

        List<Rigidbody> objects = new();

        foreach (Collider item in colliders)
            if (item.attachedRigidbody != null)
                objects.Add(item.attachedRigidbody);

        return objects;
    }
}