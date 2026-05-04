using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 20;
    [SerializeField] private int _enragedAttackDamage = 40;
    
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private LayerMask _whatIsPlayer;
	
	private Vector3 _attackOffset;
    private void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * _attackOffset.x;
		pos += transform.up * _attackOffset.y;

		Collider2D collider = Physics2D.OverlapCircle(pos, _attackRange, _whatIsPlayer);
		if (collider != null)
		{
            collider.GetComponent<PlayerStats>().DecreaseHealth(_attackDamage);
		}
	}

    private void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * _attackOffset.x;
		pos += transform.up * _attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, _attackRange, _whatIsPlayer);
		if (colInfo != null)
		{
            colInfo.GetComponent<PlayerStats>().DecreaseHealth(_enragedAttackDamage);
		}
	}

    public void PlayAttackSound()
    {
        AudioManager.Instance.PlaySFX("Spell Impact 1");
    }
    public void PlayEnragedAttackSound()
    {
        AudioManager.Instance.PlaySFX("Rock Meteor Swarm 2");
    }

    private void OnDrawGizmos()
	{
		Vector3 pos = transform.position;
		pos += transform.right * _attackOffset.x;
		pos += transform.up * _attackOffset.y;

		Gizmos.DrawWireSphere(pos, _attackRange);
	}
}
