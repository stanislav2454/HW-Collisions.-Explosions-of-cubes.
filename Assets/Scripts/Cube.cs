using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _splitChance = 1f;
    private Vector3 _initialScale;
    private Renderer _renderer;

    public float SplitChance => _splitChance;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _initialScale = transform.localScale;
        SetRandomColor();
    }

    public void SetSplitParameters(float newSplitChance)
    {
        _splitChance = newSplitChance;
        UpdateScale();
        SetRandomColor();
    }

    private void UpdateScale() =>
        transform.localScale = _initialScale * _splitChance;

    private void SetRandomColor() =>
        _renderer.material.color = Random.ColorHSV();
}
