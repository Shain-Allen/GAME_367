using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _notificationPrefab;
    [SerializeField, Min(0), Tooltip("How quickly should the notifications fade away")] private float _fadeTime = 1f;
    [SerializeField, Min(1), Tooltip("How many messages should be allowed on the screen at once")] private int _maxMessages;

    public void PickupNotification(string newNotificationMessage)
    {
        GameObject newMessage = Instantiate(_notificationPrefab, transform);
        newMessage.transform.SetSiblingIndex(0);

        // get the text field and set it to the message.
        newMessage.GetComponentInChildren<TextMeshProUGUI>().text = newNotificationMessage;
        
        // "index from end" or "range operator" to get the lastly added element from the list
        StartCoroutine(FadeNotification(newMessage));
    }

    
    private IEnumerator FadeNotification(GameObject notification)
    {
        // Cache references to needed values to aid in leaping Alpha to 0 for the fade
        
        Image notificationBackground = notification.GetComponent<Image>();
        TextMeshProUGUI notificationText = notification.GetComponentInChildren<TextMeshProUGUI>();

        Color backgroundStartingColor = notificationBackground.color;
        Color newBackgroundColor = backgroundStartingColor;

        Color textStartingColor = notificationText.color;
        Color newTextColor = textStartingColor;

        float elapsedTime = 0;
        
        // lerp alpha of text and background of message to 0 for message to fade
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