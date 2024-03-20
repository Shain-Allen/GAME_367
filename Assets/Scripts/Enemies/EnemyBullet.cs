using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            Destroy(gameObject);
            return;
        }

        playerHealth.TakeDamage(Damage);
        
        Destroy(gameObject);
    }
}