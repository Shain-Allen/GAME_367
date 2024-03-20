using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LaserWallTrap : MonoBehaviour
{
    [Header("Wall Timer Params")]
    [SerializeField] private bool _laserTimerOn;
    [SerializeField] private float _laserWallUpTime;
    [SerializeField] private float _laserWallDownTime;
    [SerializeField] private float _minVariance;
    [SerializeField] private float _maxVariance;
    private Coroutine _laserGateTimer;
    
    [Header("Wall Damage Params")]
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
        
        _laserGateTimer = StartCoroutine(GateTimer());
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
        if (_laserGateTimer != null)
        {
            StopCoroutine(_laserGateTimer);
        }
        
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
        
        _laserGateTimer = StartCoroutine(GateTimer());
    }
    
    private IEnumerator GateTimer()
    {
        while (_laserTimerOn)
        {
            _laserGate.SetActive(true);
            yield return new WaitForSeconds(_laserWallUpTime + Random.Range(_minVariance, _maxVariance));
            _laserGate.SetActive(false);
            yield return new WaitForSeconds(_laserWallDownTime + Random.Range(_minVariance, _maxVariance));
            _laserGate.SetActive(true);
        }
    }
}
