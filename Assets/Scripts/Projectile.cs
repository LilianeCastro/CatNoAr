﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    [Range(1, 20)] private float _speed = 4f;

    [SerializeField]
    [Range(1, 8)] private float _timeToDestroy = 2f;

    [SerializeField] private bool _isProjectileEnemy = false;
    
    [SerializeField] private GameObject _particleSystem;
    private Vector3 _target;

    IEnumerator Start()
    {
        if (_isProjectileEnemy)
        {
            _target = Player.Instance._bodyToAttackTarget.position;
            StartCoroutine(ProjectileEnemy());
        }
        else
        {
            StartCoroutine(ProjectilePlayer());
        }

        yield return new WaitForSeconds(_timeToDestroy);

        DestroyGameObject();
    }

    IEnumerator ProjectileEnemy()
    {
        yield return new WaitForEndOfFrame();

        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        transform.up = _target - transform.position;

        if(Vector2.Distance(_target, transform.position) <= 0)
        {
            DestroyGameObject();
        }

        StartCoroutine(ProjectileEnemy());
    }

    IEnumerator ProjectilePlayer()
    {
        yield return new WaitForEndOfFrame();

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        StartCoroutine(ProjectilePlayer());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (!_isProjectileEnemy && other.gameObject.CompareTag("Player")) { return ; }

        if (_isProjectileEnemy && other.gameObject.CompareTag("Enemy")) { return ; }

        if (damageable != null)
        {
            damageable.Damage();
            DestroyGameObject();
        }    
    }

    private void DestroyGameObject()
    {
        Destroy(_particleSystem.gameObject);
        Destroy(this.gameObject);
    }
}
