using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private BossController _boss;
    [SerializeField] private UnityEvent _onTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boss.StartBossFight();
            _onTrigger?.Invoke();
            gameObject.SetActive(false); 
        }
    }
}
