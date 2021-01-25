﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Control _control;
    private Camera _camera;

    private bool _canShoot = false;
    private bool _playerInAreaToShoot = false;

    [SerializeField] private GameObject _feedbackShootIsReady = default;
    [SerializeField] public Transform _directionToShoot = default;
    public GameObject _projectile;

    private Quaternion _initialRotation;

    [SerializeField]
    [Range(0.5f, 2.0f)]private float _minDelayShoot = 0.5f;
    [SerializeField]
    [Range(2.1f, 5.0f)]private float _maxDelayShoot = 5.0f;

    [SerializeField] private float _angleMinToRotation = -60.0f;
    [SerializeField] private float _angleMaxToRotation = 30.0f;


    private void Awake()
    {
        _control = new Control();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _control.Enable();
    }

    private void OnDisable()
    {
        _control.Disable();
    }

    private void Start()
    {
        _initialRotation = transform.rotation;
        _control.Gameplay.Shoot.performed += ctx => Shoot();
    }

    private void Update()
    {
        if (!_playerInAreaToShoot) { return ; }

        if (!_canShoot) { return ; }

        Vector2 mouseScreenPosition = _control.Gameplay.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;

        float currentAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * 90;

        if ( currentAngle > _angleMinToRotation && currentAngle < _angleMaxToRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        }        
    }

    private void Shoot()
    {
        if (!_canShoot || !_playerInAreaToShoot) { return ;}

        SetCanShoot(false);
        Instantiate(_projectile, _directionToShoot.position, _directionToShoot.rotation);
        
        GameController.Instance.SpawnShootRecovery();
        //StartCoroutine(DelayShoot());
    }

    /*private IEnumerator DelayShoot()
    {
        float delayShoot = UnityEngine.Random.Range(_minDelayShoot, _maxDelayShoot);

        yield return new WaitForSeconds(delayShoot);

        SetCanShoot(true);
    }*/

    public void SetCanShoot(bool value)
    {
        _canShoot = value;
        _feedbackShootIsReady.SetActive(_canShoot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInAreaToShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInAreaToShoot = false;
            transform.rotation = Quaternion.Euler(_initialRotation.x, _initialRotation.y, _initialRotation.z);
        }
    }
}
