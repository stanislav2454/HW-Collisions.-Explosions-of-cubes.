using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Vector3 _initialScale;
    private Renderer _renderer;

    public float SplitChance { get; private set; } = 1f;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _initialScale = transform.localScale;
        SetRandomColor();
    }

    public void Initialize(float newSplitChance)
    {
        SplitChance = newSplitChance;
        UpdateScale();
        SetRandomColor();
    }

    public List<Rigidbody> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (var item in hits)
        {
            if (item.attachedRigidbody != null)
                cubes.Add(item.attachedRigidbody);
        }

        return cubes;
    }

    private void UpdateScale() =>
        transform.localScale = _initialScale * SplitChance;

    private void SetRandomColor() =>
        _renderer.material.color = Random.ColorHSV();
}
