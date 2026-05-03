using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float _time = 300f;

    [Header("Unity Events Settings")]
    [SerializeField] private UnityEvent<float, float> _onTimerChanged;
    [SerializeField] private UnityEvent _onTimerEnd;

    private int _lastDisplayedSeconds = -1;

    private void Update()
    {
        if (_time <= 0)
        {
            _time = 0;
            _onTimerEnd.Invoke();
            return;
        }

        _time -= Time.deltaTime;

        UpdateDisplayedTime();
    }
    private void UpdateDisplayedTime()
    {
        int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(_time));
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        if (seconds != _lastDisplayedSeconds)
        {
            _lastDisplayedSeconds = seconds;
            _onTimerChanged.Invoke(minutes, seconds);
        }
    }

    public void AddTime(float amount)
    {
        _time += amount;
        UpdateDisplayedTime();
    }
}
