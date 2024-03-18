using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IShootable
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _outOfCombatTime = 5f;
    private Coroutine _outOfCombatCoroutine;
    
    [SerializeField] private float _regenerationRate = 1f;
    [SerializeField] private float _regenerationAmount = 1f;

    public int CurrentHealth { get; private set; }
    
    [SerializeField] private Slider _healthBar;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = (float)CurrentHealth/_maxHealth;
    }
    
    public void RecoverHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth > _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }
        
        _healthBar.value = (float)CurrentHealth/_maxHealth;
    }

    private void TakeDamage(int damage)
    {
        if (_outOfCombatCoroutine == null)
        {
            _outOfCombatCoroutine = StartCoroutine(OutOfCombatCoroutine());
        }
        else
        {
            StopCoroutine(_outOfCombatCoroutine);
            _outOfCombatCoroutine = StartCoroutine(OutOfCombatCoroutine());
        }
        
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        _healthBar.value = (float)CurrentHealth/_maxHealth;
    }

    public void Shot(float damage)
    {
        TakeDamage((int)damage);
    }

    private IEnumerator OutOfCombatCoroutine()
    {
        yield return new WaitForSeconds(_outOfCombatTime);
    }
    
    public IEnumerator RegenerateHealth()
    {
        // when the enemy is out of combat regenerate health
        while (_outOfCombatCoroutine == null)
        {
            RecoverHealth((int)_regenerationAmount);
            yield return new WaitForSeconds(_regenerationRate);
            
            if (CurrentHealth >= _maxHealth)
            {
                yield return null;
            }
        }
    }
}