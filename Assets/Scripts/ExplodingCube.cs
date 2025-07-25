using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(Exploder))]
public class ExplodingCube : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    //==========================================================
    //[SerializeField] private float _explosionForce = 11f;
    [SerializeField] private float _explosionRadius = 11f;

    //private float _upwardsModifier = 1f;
    //private ForceMode _forceMode = ForceMode.Impulse;
    //==========================================================

    private Renderer _renderer;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        SetRandomColor();
    }

    private void OnEnable() =>
        _raycaster.CubeHitted += TryExplodeAndDestroy;

    private void OnDisable() =>
        _raycaster.CubeHitted -= TryExplodeAndDestroy;

    private void TryExplodeAndDestroy(ExplodingCube hitCube)
    {
        //Debug.Log($"<color=red>TryExplodeAndDestroy\n(ExplodingCube hitCube)\n-</color>{hitCube.name}");
        var expObjs = hitCube.GetExplodableObjects();
        //foreach (var item in expObjs)
        //    Debug.Log($"<color=yellow>{item.name}</color>");
        if (TryGetComponent(out Exploder exploder))
        {
            //Debug.Log($"<color=cyan>exploder-</color>{exploder.name}");
            // hitCube.GetComponent<Exploder>().Explode(expObjs, hitCube.transform.position);
            exploder.Explode(expObjs, hitCube.transform.position, _explosionRadius);
        }

        Destroy(hitCube.gameObject);
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
