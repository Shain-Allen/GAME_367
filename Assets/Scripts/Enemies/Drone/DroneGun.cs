using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DroneGun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private GameObject _bulletPrefab;
    private bool _canShoot = true;
    
    public void Shoot()
    {
        if (!_canShoot) return;
        
        GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(_bulletSpawnPoint.forward * _bulletForce, ForceMode.Impulse);
        bullet.GetComponent<EnemyBullet>().Damage = _bulletDamage;
    }

    private IEnumerator ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(1 / _attackRate);
        _canShoot = true;
    }
}