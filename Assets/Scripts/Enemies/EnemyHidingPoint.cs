using UnityEngine;

public class EnemyHidingPoint : MonoBehaviour
{
    public bool IsPointVisible { get; private set; }
    
    private void OnBecameInvisible()
    {
        IsPointVisible = false;
    }
    
    private void OnBecameVisible()
    {
        IsPointVisible = true;
    }
}
