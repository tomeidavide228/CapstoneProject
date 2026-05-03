using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private GameObject _deathParticles;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_deathParticles, transform.position, _deathParticles.transform.rotation);
        GetComponent<PlayerRespawn>().Respawn();
    }
}
