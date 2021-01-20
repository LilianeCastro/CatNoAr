using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private SpriteRenderer _baloonSr;

    private void Start()
    {
        _baloonSr = GetComponent<SpriteRenderer>();
        _baloonSr.color = new Color(RandomColor(), RandomColor(), RandomColor());
        print(_baloonSr.color);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            transform.parent.GetComponent<Enemy>().CollisionDetected();
            
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }

    private float RandomColor()
    {
        return Random.Range(0f, 1.0f);
    }
}
