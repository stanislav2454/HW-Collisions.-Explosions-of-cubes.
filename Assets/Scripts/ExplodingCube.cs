using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(Exploder))]
public class ExplodingCube : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private float _explosionRadius = 11f;

    private Renderer _renderer;
    private float _splitChance = 1f;

    public float SplitChance => _splitChance;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        SetRandomColor();
    }

    private void OnEnable() =>
        _raycaster.ExplodingCubeHitted += TryExplodeAndDestroy;

    private void OnDisable() =>
        _raycaster.ExplodingCubeHitted -= TryExplodeAndDestroy;

    private void TryExplodeAndDestroy(ExplodingCube hittedCube)
    {
        var explodedObjects = hittedCube.GetExplodableObjects();

        if (TryGetComponent(out Exploder exploder))
            exploder.Explode(explodedObjects, hittedCube.transform.position);

        Destroy(hittedCube.gameObject);
    }

    private void SetRandomColor() =>
        _renderer.material.color = Random.ColorHSV();

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (var item in hits)
        {
            if (item.attachedRigidbody != null)
                cubes.Add(item.attachedRigidbody);
        }

        return cubes;
    }
}
