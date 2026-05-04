using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private GameObject _deathParticles;

    [SerializeField] private UnityEvent<float, float> _onHPChanged;
    [SerializeField] private UnityEvent _onDefeated;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        AudioManager.Instance.PlaySFX("Hurt");
        _currentHealth -= amount;
        _onHPChanged.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _onHPChanged.Invoke(_currentHealth, _maxHealth);
        Debug.Log("Player Healed");
    }


    private void Die()
    {
        Instantiate(_deathParticles, transform.position, _deathParticles.transform.rotation);
        _onDefeated.Invoke();
    }
}
