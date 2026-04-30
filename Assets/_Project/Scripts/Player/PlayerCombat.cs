using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private bool _combatOn;
    [SerializeField] private float _inputTimer;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackDamage;
    [SerializeField] private Transform _hitBoxPosition;
    [SerializeField] private LayerMask _isDamageable;

    private float[] _attackDetails = new float[2];

    private bool _gotInput;
    private bool _isAttacking;
    private float _lastInputTime;

    private Animator _anim;
    private PlayerController _playerController;
    private PlayerStats _HP;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("canAttack", _combatOn);
        _playerController = GetComponent<PlayerController>();
        _HP = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_combatOn)
            {
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if (_gotInput)
        {
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _anim.SetBool("attack", true);
                _anim.SetBool("isAttacking", _isAttacking);
            }
        }

        if (Time.time >= _lastInputTime + _inputTimer)
        {
            _gotInput = false;
        }
    }

    private void CheckHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_hitBoxPosition.position, _attackRadius, _isDamageable);

        _attackDetails[0] = _attackDamage;
        _attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects)
        {
            // Base Enemy
            collider.transform.parent?.SendMessage("Damage", _attackDetails, SendMessageOptions.DontRequireReceiver);

            // Boss
            BossHealth boss = collider.GetComponentInParent<BossHealth>();
            if (boss != null)
            {
                boss.Damage(_attackDetails);
            }

        }
    }

    private void FinishAttack()
    {
        _isAttacking = false;
        _anim.SetBool("isAttacking", _isAttacking);
        _anim.SetBool("attack", false);
    }

    private void Damage(float[] attackDetails)
    {
            int direction;

            _HP.DecreaseHealth(attackDetails[0]);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            _playerController.Knockback(direction);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_hitBoxPosition.position, _attackRadius);
    }
}
