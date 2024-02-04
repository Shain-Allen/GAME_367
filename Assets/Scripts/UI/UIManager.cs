using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private NotificationDisplay _notificationDisplay;
    [SerializeField] private ToolTipDisplay _toolTipDisplay;

    //custom delegate to allow C# event to have default string value
    public delegate void DisplayToolTipEvent(bool displayToolTip, string toolTipMessage = "");
    
    public static Action<float, float> UpdateHealthUI;
    public static Action<int> UpdateAmmoUI;
    public static Action<string> NewNotification;
    public static DisplayToolTipEvent DisplayToolTipUI;

    //subscribe to C# events declared above to allow for UI updates
    private void Awake()
    {
        UpdateHealthUI += UpdateHealth;
        UpdateAmmoUI += UpdateAmmo;
        NewNotification += _notificationDisplay.PickupNotification;
        DisplayToolTipUI += _toolTipDisplay.DisplayToolTip;
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth / maxHealth;
    }

    private void UpdateAmmo(int currentAmmo)
    {
        _ammoText.text = $"Ammo: {(currentAmmo < 10 ? "0" + currentAmmo : currentAmmo)}";
    }

    
}
