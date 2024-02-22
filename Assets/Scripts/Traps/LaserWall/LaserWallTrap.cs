using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWallTrap : MonoBehaviour
{
    [SerializeField] private float _lasweWallUpTime;
    [SerializeField] private float _laserWallDownTime;
    [SerializeField] private float _laserWallFixTime;
    [SerializeField, Min(0)] private int _damageLevels = 2;
    private int _currentDamageLevel = 0;
    [SerializeField] private List<Color> _damageColors;
    
    private GameObject _laserGate;
    
    public Action LaserEmitterShot;
    public Action<Color> DamageLevelChanged;

    private void Awake()
    {
        LaserEmitterShot += WallLaserEmitterShot;

        _laserGate = GetComponentInChildren<LaserWall>().gameObject;

        _damageColors ??= new List<Color>() { Color.blue };
        
        DamageLevelChanged?.Invoke(_damageColors[0]);
    }

    private void  WallLaserEmitterShot()
    {
        if(!_laserGate.activeSelf) return;
        
        _currentDamageLevel++;
        DamageLevelChanged?.Invoke(_damageColors[Mathf.Min(_damageColors.Count - 1, _currentDamageLevel)]);
        Debug.Log($"Laser Wall damage level {_currentDamageLevel}/{_damageLevels}");
        
        if (_currentDamageLevel < _damageLevels) return;
        DamageLevelChanged?.Invoke(_damageColors[Mathf.Min(_damageColors.Count - 1, _currentDamageLevel)]);
        StartCoroutine(FixGate());
    }

    private IEnumerator FixGate()
    {
        _laserGate.SetActive(false);

        while (_currentDamageLevel != 0)
        {
            yield return new WaitForSeconds(_laserWallFixTime/_damageLevels);
            _currentDamageLevel--;
            DamageLevelChanged?.Invoke(_damageColors[_currentDamageLevel]);
        }
        
        _currentDamageLevel = 0;
        DamageLevelChanged?.Invoke(_damageColors[_currentDamageLevel]);
        _laserGate.SetActive(true);
    }
}
