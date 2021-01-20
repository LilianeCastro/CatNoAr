using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _enemyRb;

    [SerializeField] private Animator _manAnim = default;

    [SerializeField] private SpriteRenderer _projectileShowSr = default;
    [SerializeField] private GameObject _projectileEnemyPrefab = default;
    [SerializeField] private Transform _posHandEnemyToShoot = default;

    [SerializeField]
    [Range(0.01f, 0.075f)] private float _speedEnemyToIncrement = 0.05f;

    private float _speedEnemy = 0.05f;
    private bool _canMove = true;
    
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();

        StartCoroutine(AttackEnemy());

        int randonMove = Random.Range(0, 100);
        Coroutine initialMovement = randonMove > 50 ? StartCoroutine(MoveToRight()) : StartCoroutine(MoveToLeft());
        
    }

    IEnumerator AttackEnemy()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator MoveToRight()
    {
        yield return new WaitForSeconds(0.01f);
        
        if (_canMove) {
            if (transform.position.x <= 8f)
            {
                transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x + _speedEnemy, transform.position.y), 0.5f);
                StartCoroutine(MoveToRight());
            }
            else
            {
                StopCoroutine(MoveToRight());
                StartCoroutine(MoveToLeft());
            }
        }
        else
        {
            StopCoroutine(MoveToRight());
        }          
    }

    IEnumerator MoveToLeft()
    {    
        yield return new WaitForSeconds(0.01f);

        if (_canMove)
        {
            if (transform.position.x >= 0f)
            {
                transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x -_speedEnemy, transform.position.y), 0.5f);
                StartCoroutine(MoveToLeft());
            }
            else
            {
                StopCoroutine(MoveToLeft());
                StartCoroutine(MoveToRight());
            }
        }
        else
        {
            StopCoroutine(MoveToLeft());
        }       
    }

    public void CollisionDetected()
    {      
        _speedEnemy += _speedEnemyToIncrement;

        int ballonCount = GameObject.FindObjectsOfType<Balloon>().Length;

        if (ballonCount > 1)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            _manAnim.SetTrigger("BalloonDamage");
        }
        else
        {
            _canMove = false;
            
            _enemyRb.gravityScale = 1.0f;
            _manAnim.SetTrigger("NoBalloons");
        }    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _manAnim.SetTrigger("OnGround");
    }

}
