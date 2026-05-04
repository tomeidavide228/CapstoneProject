using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 200;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _secondPhaseHealth = 100;

    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private GameObject _deathParticles;

    [SerializeField] private UnityEvent _onBossDefeat;

    private bool _isInvulnerable = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void SetIsInvulnerable(bool isInvulnerable)
    {
        _isInvulnerable = isInvulnerable;

    }

    public void Damage(float[] attackDetails)
    {
        if (_isInvulnerable)
        {
            return;
        }

        int damage = (int)attackDetails[0];
        float attackerX = attackDetails[1];

        _currentHealth -= damage;

        AudioManager.Instance.PlaySFX("Hurt");
        Instantiate(_hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (_currentHealth <= _secondPhaseHealth)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);

        }
        if (_currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Instantiate(_deathParticles, transform.position, _deathParticles.transform.rotation);
        AudioManager.Instance.PlayMusic("Strange Worlds");
        _onBossDefeat?.Invoke();
        Destroy(gameObject);
    }

}
