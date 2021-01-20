using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    [Range(1, 20)] private float speed = 4f;

    [SerializeField]
    [Range(1, 4)] private float timeToDestroy = 2f;
    
    [SerializeField]
    [Range(1, 4)] private int damageAmount = 1;


    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(this.gameObject);
    }
    
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable<int> damageable = other.GetComponent<IDamageable<int>>();

        if (damageable != null)
        {
            damageable.Damage(damageAmount);
            Destroy(this.gameObject);
        }    
    }
}
