using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _splitChance = 1f;
    private Vector3 _initialScale;

    public float SplitChance => _splitChance;

    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        _initialScale = transform.localScale;
    }

    public void SetSplitParameters(float newSplitChance)
    {
        _splitChance = newSplitChance;
        UpdateScale();
    }

    private void UpdateScale()
    {
        transform.localScale = _initialScale * _splitChance;
    }
}
