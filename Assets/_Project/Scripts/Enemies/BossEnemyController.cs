using UnityEngine;

public enum BossState
{
    Moving,
    Knockback,
    Attack1,
    Attack2,
    PhaseChange,
    Dead
}

public class BossController : MonoBehaviour
{
    private BossState currentState;

    [Header("Stats")]
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private float _wallCheckDistance;

    [Header("Knockback")]
    [SerializeField] private float _knockbackDuration;
    [SerializeField] private Vector2 _knockbackSpeed;
    private float _knockbackStartTime;
    private int _damageDirection;

    [Header("References")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private GameObject _deathParticles;

    private Rigidbody2D _rb;
    private Animator _anim;
    private int _facingDirection = 1;

    private float[] attackDetails = new float[2];

    private void Start()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

        _currentHealth = _maxHealth;

        SwitchState(BossState.Moving);
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Moving:
                UpdateMoving();
                break;

            case BossState.Knockback:
                UpdateKnockback();
                break;

            case BossState.Attack1:
                break;

            case BossState.Attack2:
                break;

            case BossState.PhaseChange:
                break;

            case BossState.Dead:
                break;
        }
    }

    private void UpdateMoving()
    {
        bool groundDetected = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance);
        bool wallDetected = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance);

        if (!groundDetected || wallDetected)
            Flip();
        else
            _rb.velocity = new Vector2(_moveSpeed * _facingDirection, _rb.velocity.y);
    }

    private void UpdateKnockback()
    {
        if (Time.time >= _knockbackStartTime + _knockbackDuration)
            SwitchState(BossState.Moving);
    }

    public void Damage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Instantiate(_hitParticle, transform.position, Quaternion.identity);

        _damageDirection = attackDetails[1] > transform.position.x ? -1 : 1;

        if (_currentHealth > 0)
        {
            SwitchState(BossState.Knockback);
        }
        else
        {
            SwitchState(BossState.Dead);
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void SwitchState(BossState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case BossState.Knockback:
                _knockbackStartTime = Time.time;
                _rb.velocity = new Vector2(_knockbackSpeed.x * _damageDirection, _knockbackSpeed.y);
                _anim.SetTrigger("Hit");
                break;

            case BossState.Dead:
                Instantiate(_deathParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }
}
