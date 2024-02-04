using System.Collections;
using UnityEngine;

public interface IPickup
{
    public void Interact(GameObject other);

    public IEnumerator PickupReset(float resetTime);
}