using System.Collections;
using UnityEngine;

public class CubeColor : MonoBehaviour, IShootable
{
    [SerializeField] private Color _defaultColor = Color.green;
    [SerializeField] private Color _shotColor = Color.red;

    private Renderer _renderer;

    [SerializeField] private float _resetTimer = 30;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.color = _defaultColor;
    }

    public void Shot()
    {
        _renderer.material.color = _shotColor;
        StartCoroutine(ResetColor());
    }
    
    //resets the color of the object back starter color
    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(_resetTimer);
        _renderer.material.color = _defaultColor;
    }
}