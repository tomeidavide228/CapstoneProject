using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [Header("UI Timer Settings")]
    [SerializeField] TextMeshProUGUI _timerText;

    public void UpdateGraphics(float minutes, float seconds)
    {
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
