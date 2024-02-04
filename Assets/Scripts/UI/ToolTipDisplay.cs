using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipDisplay : MonoBehaviour
{
    private Image _toolTipBg;
    private TextMeshProUGUI _toolTipText;

    private void Awake()
    {
        _toolTipBg = GetComponent<Image>();
        _toolTipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayToolTip(bool displayToolTip, string toolTipMessage)
    {
        _toolTipBg.enabled = displayToolTip;
        _toolTipText.text = toolTipMessage;
    }
}