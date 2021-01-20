using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _enemyRb;

    [SerializeField] private Animator _manAnim = default;

    [SerializeField] private GameObject _projectileEnemyPrefab = default;
    [SerializeField] private Transform _posHandEnemyToShoot = default;

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        print(_manAnim);
    }

    public void CollisionDetected()
    {
        
        int ballonCount = GameObject.FindObjectsOfType<Balloon>().Length;
        if (ballonCount > 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            _manAnim.SetTrigger("BalloonDamage");
        }
        else
        {
            _enemyRb.gravityScale = 1.0f;
            _manAnim.SetTrigger("NoBalloons");
        }    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _manAnim.SetTrigger("OnGround");
    }
}
