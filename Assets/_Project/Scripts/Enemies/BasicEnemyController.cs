using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State
{
    Moving,
    Knockback,
    Dead
}
public class BasicEnemyController : MonoBehaviour
{


    private State currentState;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _knockbackDuration;
    [SerializeField] private float _lastTouchDamageTime;
    [SerializeField] private float _touchDamageCooldown;
    [SerializeField] private float _touchDamage;
    [SerializeField] private float _touchDamageWidth;
    [SerializeField] private float _touchDamageHeight;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Transform _touchDamageCheck;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsPlayer;

    [SerializeField] private Vector2 _knockbackSpeed;

    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private GameObject _deathParticles;

    private float _currentHealth;
    private float _knockbackStartTime;

    private float[] _attackDetails = new float[2];

    private int _facingDirection;
    private int _damageDirection;

    private Vector2 _movement;
    private Vector2 _touchDamageBotLeft;
    private Vector2 _touchDamageTopRight;

    private bool _groundDetected;
    private bool _wallDetected;

    private GameObject _enemy;
    private Rigidbody2D _enemyRb;
    private Animator _EnemtAnim;

    private void Start()
    {
        _enemy = transform.Find("Enemy").gameObject;
        _enemyRb = _enemy.GetComponent<Rigidbody2D>();
        _EnemtAnim = _enemy.GetComponent<Animator>();

        _currentHealth = _maxHealth;
        _facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        _groundDetected = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);
        _wallDetected = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance, _whatIsGround);

        CheckTouchDamage();

        if (!_groundDetected || _wallDetected)
        {
            Flip();
        }
        else
        {
            _movement.Set(_movementSpeed * _facingDirection, _enemyRb.velocity.y);
            _enemyRb.velocity = _movement;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        _knockbackStartTime = Time.time;
        _movement.Set(_knockbackSpeed.x * _damageDirection, _knockbackSpeed.y);
        _enemyRb.velocity = _movement;
        _EnemtAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= _knockbackStartTime + _knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        _EnemtAnim.SetBool("Knockback", false);
    }

    private void EnterDeadState()
    {
        Instantiate(_deathParticles, _enemy.transform.position, _deathParticles.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void Damage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Instantiate(_hitParticle, _enemy.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > _enemy.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        if (_currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (_currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= _lastTouchDamageTime + _touchDamageCooldown)
        {
            _touchDamageBotLeft.Set(_touchDamageCheck.position.x - (_touchDamageWidth / 2), _touchDamageCheck.position.y - (_touchDamageHeight / 2));
            _touchDamageTopRight.Set(_touchDamageCheck.position.x + (_touchDamageWidth / 2), _touchDamageCheck.position.y + (_touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(_touchDamageBotLeft, _touchDamageTopRight, _whatIsPlayer);

            if (hit != null)
            {
                _lastTouchDamageTime = Time.time;
                _attackDetails[0] = _touchDamage;
                _attackDetails[1] = _enemy.transform.position.x;
                hit.SendMessage("Damage", _attackDetails);
            }
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        _enemy.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector2(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));

        Vector2 botLeft = new Vector2(_touchDamageCheck.position.x - (_touchDamageWidth / 2), _touchDamageCheck.position.y - (_touchDamageHeight / 2));
        Vector2 botRight = new Vector2(_touchDamageCheck.position.x + (_touchDamageWidth / 2), _touchDamageCheck.position.y - (_touchDamageHeight / 2));
        Vector2 topRight = new Vector2(_touchDamageCheck.position.x + (_touchDamageWidth / 2), _touchDamageCheck.position.y + (_touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(_touchDamageCheck.position.x - (_touchDamageWidth / 2), _touchDamageCheck.position.y + (_touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

}