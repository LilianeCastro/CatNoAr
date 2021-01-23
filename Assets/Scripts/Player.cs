using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Animator))
]

public class Player : Singleton<Player>, IDamageable
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;
    private Vector3 _initialPositionToRespawn;

    private float _horizontalInput;
    private bool _isGrounded = true;
    private bool _isLookLeft = false;

    public Transform _groundCheckLeft;
    public Transform _groundCheckRight;
    public float _distance;

    public float _playerSpeed;
    public float _playerForceJump;
    
    public override void Init()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _playerRb.velocity = new Vector2(_horizontalInput * _playerSpeed, _playerRb.velocity.y);
        
        _isGrounded = Physics2D.Raycast(_groundCheckLeft.position, Vector2.down, _distance, 1 << LayerMask.NameToLayer("Ground"))
        || Physics2D.Raycast(_groundCheckRight.position, Vector2.down, _distance, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnMove(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;

        if (_isLookLeft && _horizontalInput > 0)
        {
            Flip();
        }
        else if (!_isLookLeft && _horizontalInput < 0)
        {
            Flip();
        }

        _playerAnim.SetInteger("Speed", (int)_horizontalInput);
    }

    private void Flip()
    {
        _isLookLeft = !_isLookLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnJump(InputValue value)
    {
        if(value.isPressed && _isGrounded)
        {
            _playerRb.AddForce(new Vector2(_playerRb.velocity.x, _playerForceJump));
        }
    }

    public void Damage()
    {
        GameController.Instance.ScorePlayer = -1;
    }
}
