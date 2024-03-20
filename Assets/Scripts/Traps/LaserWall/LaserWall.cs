using System;
using System.Collections;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damageAmount;
    [SerializeField, Min(0)] private float _damageSpeed;
    private bool _playerInWall = false;

    private Coroutine _dealDamageRef;

    
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out PlayerHealth player)) return;
        
        _playerInWall = true;

        _dealDamageRef = StartCoroutine(DealDamage(player));
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInWall = false;
    }

    private IEnumerator DealDamage(PlayerHealth playerHealth)
    {
        while (_playerInWall)
        {
            playerHealth.TakeDamage(_damageAmount);

            yield return new WaitForSeconds(_damageSpeed);
        }
    }

    private void OnDisable()
    {
        if (_dealDamageRef == null) return;
        
        StopCoroutine(_dealDamageRef);
    }
    
    
}