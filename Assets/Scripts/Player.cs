using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;

    public float _playerSpeed;
    public float _playerForceJump;
    
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnHorizontalMove(InputValue value)
    {
        print(value.Get<float>());
    }
}
