using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private float _shootDistance;

    [SerializeField] private PlayerAmmo _playerAmmo;

    private void Awake()
    {
        _playerAmmo = transform.GetComponentInParent<PlayerAmmo>();
    }

    public void HandleRaycast()
    {
        // Detect if the fire button is pressed
        if (!Input.GetButtonDown("Fire1")) return;

        //cast the way and check if anything was hit
        if (!Physics.Raycast(_cam.transform.position, _cam.forward, out RaycastHit hit, _shootDistance)) return;
        
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
}