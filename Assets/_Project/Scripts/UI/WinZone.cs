using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinZone : MonoBehaviour
{
    [Header("Win Event Settings")]
    [SerializeField] private UnityEvent _onEnterWinZone;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.CompareTag("Player"))
        {
            _onEnterWinZone?.Invoke();
        }
    }
}
