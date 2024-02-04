using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _currentHealth = 0;
    [SerializeField] private float _maxHealth = 0;

    private void Awake()
    {
        StartCoroutine(DelayedInitialization());
    }

    public bool HealPlayer(float healthAmount)
    {
        if (_currentHealth + healthAmount >= _maxHealth) return false;

        _currentHealth = Mathf.Min(_currentHealth + healthAmount, _maxHealth);
        UIManager.UpdateHealthUI(_currentHealth, _maxHealth);
        UIManager.NewNotification($"picked up {healthAmount} health");
        return true;
    }

    public bool TakeDamage(float damageAmount)
    {
        _currentHealth = Mathf.Max(_currentHealth - damageAmount, 0f);
        UIManager.UpdateHealthUI(_currentHealth, _maxHealth);
        return true;
    }
    
    //Delay reference to the UI manager so that the Static C# events can be ready to receive info
    private IEnumerator DelayedInitialization()
    {
        yield return new WaitForSeconds(0.1f);

        // Perform initialization logic here
        UIManager.UpdateHealthUI(_currentHealth, _maxHealth);
    }
}