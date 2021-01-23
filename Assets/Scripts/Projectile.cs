using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    [Range(1, 20)] private float _speed = 4f;

    [SerializeField]
    [Range(1, 6)] private float _timeToDestroy = 2f;

    [SerializeField] private bool _isProjectileEnemy;
    
    private Vector3 _target;

    IEnumerator Start()
    {
        if (_isProjectileEnemy)
        {
            _target = Player.Instance.transform.position;
            StartCoroutine(ProjectileEnemy());
        }
        else
        {
            StartCoroutine(ProjectilePlayer());
        }

        yield return new WaitForSeconds(_timeToDestroy);

        Destroy(this.gameObject);
    }

    IEnumerator ProjectileEnemy()
    {
        yield return new WaitForEndOfFrame();

        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        transform.up = _target - transform.position;

        if(Vector2.Distance(_target, transform.position) <= 0)
        {
            Destroy(this.gameObject);
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

        if (damageable != null)
        {
            damageable.Damage();
            Destroy(this.gameObject);
        }    
    }
}
