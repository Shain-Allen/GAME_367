using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnCollisionEnter(Collision other)
    {
        if (! other.gameObject.TryGetComponent(out PlayerHealth playerHealth)) return;

        playerHealth.TakeDamage(Damage);
        
        Destroy(gameObject);
    }
}