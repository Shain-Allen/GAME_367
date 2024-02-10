using System;
using UnityEngine;

public class LaserEmitter : MonoBehaviour, IShootable
{
    private LaserWallTrap _laserWallTrap;
    
    private void Awake()
    {
        _laserWallTrap = GetComponentInParent<LaserWallTrap>();
    }
    
    public void Shot()
    {
        _laserWallTrap.LaserEmitterShot();
    }
}