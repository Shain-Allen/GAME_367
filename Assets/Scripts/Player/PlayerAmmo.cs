using System.Collections;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField, Min(0)] private int _currentAmmo = 10;
    [SerializeField, Range(0, 99)] private int _maxAmmo = 99;

    private void Awake()
    {
        StartCoroutine(DelayedInitialization());
    }

    public bool AddAmmo(int ammoAmount)
    {
        if (_currentAmmo == _maxAmmo) return false;

        _currentAmmo = Mathf.Min(_currentAmmo + ammoAmount, _maxAmmo);
        UIManager.UpdateAmmoUI(_currentAmmo);
        UIManager.NewNotification($"picked up {ammoAmount} ammo");
        return true;
    }

    public bool Shoot()
    {
        if (_currentAmmo == 0) return false;
        _currentAmmo--;
        UIManager.UpdateAmmoUI(_currentAmmo);
        return true;
    }
    
    //Delay reference to the UI manager so that the Static C# events can be ready to receive info
    private IEnumerator DelayedInitialization()
    {
        yield return new WaitForSeconds(0.1f);

        // Perform initialization logic here
        UIManager.UpdateAmmoUI(_currentAmmo);
    }
}