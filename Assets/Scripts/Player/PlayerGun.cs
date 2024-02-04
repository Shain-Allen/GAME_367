using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private float _shootDistance;
    [SerializeField] private float _interactDistance;

    [SerializeField] private PlayerAmmo _playerAmmo;

    private void Awake()
    {
        _playerAmmo = transform.GetComponentInParent<PlayerAmmo>();
    }

    public void Shoot()
    {
        // Detect if the fire button is pressed
        if (!Input.GetButtonDown("Fire1")) return;

        //cast the way and check if anything was hit
        if (!Physics.Raycast(_cam.position, _cam.forward, out RaycastHit hit, _shootDistance)) return;
        
        //don't let the player shoot if they are out of ammo
        if (_playerAmmo && !_playerAmmo.Shoot()) return; 
        
        //Check if what was hit is a Pickup
        if (hit.transform.TryGetComponent(out IPickup pickup))
        {
            pickup.Interact(transform.parent.gameObject);
        }
        
        //check if what was hit is a Shootable object
        if (hit.transform.TryGetComponent(out IShootable hitObject))
        {
            hitObject.Shot();
        }
    }

    // continuously shoot out a ray to check for if a item is usable
    public void CheckForInteracables()
    {
        //cast a ray to see if anything is in front of the player
        if (!Physics.Raycast(_cam.position, _cam.forward, out RaycastHit hit, _interactDistance))
        {
            UIManager.DisplayToolTipUI(false);
            return;
        }

        if (hit.transform.TryGetComponent(out IPickup pickup))
        {
            UIManager.DisplayToolTipUI(true, "Press E to pickup");
            
            // detect if the Interact button is pressed
            if (!Input.GetKeyDown(KeyCode.E)) return;
                pickup.Interact(transform.parent.gameObject);
        }
        else
        {
            UIManager.DisplayToolTipUI(false);
        }
        
        
    }
}