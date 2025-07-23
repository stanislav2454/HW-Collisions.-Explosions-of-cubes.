using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const float Delay = 0.1f;

    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 8f;

    //[SerializeField] private GameObject _cube;
    private Coroutine _coroutine;
    public void Explode()
    {
        Vector3 originalPosition = transform.position;

        foreach (Rigidbody item in GetExplodableObjects(transform.position, _explosionRadius))
            //item.AddExplosionForce(_explosionForce, originalPosition, _explosionRadius);
            _coroutine = StartCoroutine(DelayExplode(item, originalPosition));
        //if (_cube.TryGetComponent<Rigidbody>(out var rb))
        //{
        //    Vector3 originalPosition = transform.position;
        //    _coroutine = StartCoroutine(DelayExplode(rb, originalPosition));
        //    // DelayExplode(rb, originalPosition);
        //    //rb.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
        //}
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

    private IEnumerator DelayExplode(Rigidbody rigidbody, Vector3 explosionCenter)
    {
        var wait = new WaitForSeconds(Delay);

        //while (enabled)
        //{
        //    yield return wait;

        //    rigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
        //}
        yield return wait;
        rigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
    }
}