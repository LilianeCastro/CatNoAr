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
    }

    private float RandomColor()
    {
        return Random.Range(0f, 1.0f);
    }

    public void Damage(int damageTaken)
    {
        transform.parent.GetComponent<Enemy>().CollisionDetected();
        
        ParticleSystem.MainModule part = transform.parent.GetComponent<Enemy>()._balloonVfxPrefab.main;
        part.startColor = new Color(_baloonSr.color.r, _baloonSr.color.g, _baloonSr.color.b, 0.75f);

        Instantiate(transform.parent.GetComponent<Enemy>()._balloonVfxPrefab, transform.position, transform.rotation);

        Destroy(this.gameObject);        
    }
}
