using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour, IDamageable<int>
{
    private SpriteRenderer _baloonSr;

    private void Start()
    {
        _baloonSr = GetComponent<SpriteRenderer>();
        _baloonSr.color = new Color(RandomColor(), RandomColor(), RandomColor());
        print(_baloonSr.color);
    }

    private float RandomColor()
    {
        return Random.Range(0f, 1.0f);
    }

    public void Damage(int damageTaken)
    {
        transform.parent.GetComponent<Enemy>().CollisionDetected();   
        Destroy(this.gameObject);        
    }
}
