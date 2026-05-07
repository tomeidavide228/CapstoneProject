using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("mine 1");
            collision.GetComponent<PlayerStats>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
