using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 9f;
    [SerializeField] private float _explosionRadius = 8f;

    private float _upwardsModifier = 1f;
    private ForceMode _forceMode = ForceMode.Impulse;

    public void Explode(GameObject[] objects)
    {
        Vector3 explosionPosition = transform.position;

        foreach (var rigidbody in objects)
        {
            rigidbody.GetComponent<Rigidbody>().AddExplosionForce(
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