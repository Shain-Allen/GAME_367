using System;
using System.Collections;
using UnityEngine;

public class LaserWallTrap : MonoBehaviour
{
    [SerializeField] private float _lasweWallUpTime;
    [SerializeField] private float _laserWallDownTime;
    [SerializeField] private float _laserWallFixTime;

    private GameObject _laserGate;
    
    public Action LaserEmitterShot;

    private void Awake()
    {
        LaserEmitterShot += LaserEmitterDown;

        _laserGate = GetComponentInChildren<LaserWall>().gameObject;
    }

    private void LaserEmitterDown()
    {
        if(!_laserGate.activeSelf) return;
        
        StartCoroutine(FixGate());
    }

    private IEnumerator FixGate()
    {
        _laserGate.SetActive(false);

        yield return new WaitForSeconds(_laserWallFixTime);
        
        _laserGate.SetActive(true);
    }
}
