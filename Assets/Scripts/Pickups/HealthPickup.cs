using System;
using System.Collections;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickup
{
    [SerializeField, Min(0)] private float _healthPickupAmount;
    [SerializeField, Min(0)] private float _resetTime;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Interact(GameObject other)
    {
        
        if(!other.TryGetComponent(out PlayerHealth playerHealth)) return;
        
        if(!playerHealth.HealPlayer(_healthPickupAmount)) return;
        
        StartCoroutine(PickupReset(_resetTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact(other.gameObject);
    }
    
    public IEnumerator PickupReset(float resetTime)
    {
        _collider.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        
        if(resetTime == 0) Destroy(gameObject);

        yield return new WaitForSeconds(resetTime);

        transform.GetChild(0).gameObject.SetActive(true);
        _collider.enabled = true;
    }
}