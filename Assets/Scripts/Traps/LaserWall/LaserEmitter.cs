using UnityEngine;

public class LaserEmitter : MonoBehaviour, IShootable
{
    private LaserWallTrap _laserWallTrap;
    private Renderer _laserEmitterRenderer;
    
    private void Awake()
    {
        _laserWallTrap = GetComponentInParent<LaserWallTrap>();
        _laserEmitterRenderer = GetComponentInChildren<Renderer>();
        _laserWallTrap.DamageLevelChanged += DamageLevelChanged;
    }

    private void DamageLevelChanged(Color damageColor)
    {
        _laserEmitterRenderer.material.color = damageColor;
    }

    public void Shot()
    {
        _laserWallTrap.LaserEmitterShot();
    }
}