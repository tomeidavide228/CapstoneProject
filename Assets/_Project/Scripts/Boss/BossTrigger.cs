using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private Boss _boss; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _boss.StartBossFight();
            gameObject.SetActive(false); 
        }
    }
}
