using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _jumpForce = 12f;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _knockbackDuration;
    [SerializeField] private Vector2 _knockbackSpeed;

    private float _move;
    private float _variableJumpHeight;
    private bool _isGrounded;
    private bool _isWalking;
    private bool _knockback;
    private bool _canFlip = true;
    private bool _isFacingRight = true;
    private float _knockbackStartTime;

    private Rigidbody2D _rb;
    private Animator _anim;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveInput();
        CheckMovementDirection();
        UpdateAnimations();
        Jump();
        CheckGround();
        CheckKnockback();

    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void MoveInput()
    {
        _move = Input.GetAxis("Horizontal");
    }

    private void ApplyMovement()
    {
        if (!_knockback)
        {
            _rb.velocity = new Vector2(_move * _moveSpeed, _rb.velocity.y);
        }


    }

    private void CheckMovementDirection()
    {
        if (_isFacingRight && _move < 0)
        {
            Flip();
        }
        else if (!_isFacingRight && _move > 0)
        {
            Flip();
        }

        if (Mathf.Abs(_rb.velocity.x) >= 0.01f)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector2.up * Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        }
        if (Input.GetButtonUp("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _variableJumpHeight);
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer);
    }

    public void Knockback(int direction)
    {
        _knockback = true;
        _knockbackStartTime = Time.time;
        _rb.velocity = new Vector2(_knockbackSpeed.x * direction, _knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= _knockbackStartTime + _knockbackDuration && _knockback)
        {
            _knockback = false;
            _rb.velocity = new Vector2(0.0f, _rb.velocity.y);
        }
    }
    private void UpdateAnimations()
    {
        _anim.SetBool("isWalking", _isWalking);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("yVelocity", _rb.velocity.y);
    }

    public void DisableFlip()
    {
        _canFlip = false;
    }

    public void EnableFlip()
    {
        _canFlip = true;
    }

    private void Flip()
    {
        if (_canFlip && !_knockback)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundRadius);
    }
}
