using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _ammoText;
    
    [SerializeField] private GameObject _pickupMessageContainer;
    private List<GameObject> _pickupMessages;
    [SerializeField] private GameObject _notificationPrefab;
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private int _maxMessages;

    public static Action<float, float> UpdateHealthUI;
    public static Action<int> UpdateAmmoUI;
    public static Action<string> NewNotification;

    //subscribe to C# events declared above to allow for UI updates
    private void Awake()
    {
        _pickupMessages = new List<GameObject>();
        UpdateHealthUI += UpdateHealth;
        UpdateAmmoUI += UpdateAmmo;
        NewNotification += PickupNotification;
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth / maxHealth;
    }

    private void UpdateAmmo(int currentAmmo)
    {
        _ammoText.text = $"Ammo: {(currentAmmo < 10 ? "0" + currentAmmo : currentAmmo)}";
    }

    private void PickupNotification(string newNotificationMessage)
    {
        GameObject newMessage = Instantiate(_notificationPrefab, _pickupMessageContainer.transform);
        newMessage.transform.SetSiblingIndex(0);
        
        // add a new message object to the message container
        _pickupMessages.Add(newMessage);

        // get the text field and set it to the message.
        newMessage.GetComponentInChildren<TextMeshProUGUI>().text = newNotificationMessage;
        
        // "index from end" or "range operator" to get the lastly added element from the list
        StartCoroutine(FadeNotification(newMessage));
    }

    
    private IEnumerator FadeNotification(GameObject notification)
    {
        Image notificationBackground = notification.GetComponent<Image>();
        TextMeshProUGUI notificationText = notification.GetComponentInChildren<TextMeshProUGUI>();

        Color backgroundStartingColor = notificationBackground.color;
        Color newBackgroundColor = backgroundStartingColor;

        Color textStartingColor = notificationText.color;
        Color newTextColor = textStartingColor;

        float elapsedTime = 0;
        
        while (elapsedTime < _fadeTime)
        {
            if (notification.transform.GetSiblingIndex() > _maxMessages)
            {
                Destroy(notification);
                yield break;
            }
            
            newBackgroundColor.a = Mathf.Lerp(backgroundStartingColor.a , 0, elapsedTime / _fadeTime);
            notificationBackground.color = newBackgroundColor;
            
            newTextColor.a = Mathf.Lerp(textStartingColor.a , 0, elapsedTime / _fadeTime);
            notificationText.color = newTextColor;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(notification);
    }
}
