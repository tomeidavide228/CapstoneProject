using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 150;
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private float _currentHealth;

    private bool _isInvulnerable = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

	public void SetIsInvulnerable( bool isInvulnerable)
	{
        _isInvulnerable = isInvulnerable;

    }

    public void Damage(float[] attackDetails)
    {
        if (_isInvulnerable)
            return;

        int damage = (int)attackDetails[0];
        float attackerX = attackDetails[1];

        _currentHealth -= damage;

        if (_currentHealth <= 60)
            GetComponent<Animator>().SetBool("IsEnraged", true);

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
