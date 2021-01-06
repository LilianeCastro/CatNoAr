using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    [Range(1, 20)] private float speed = 4f;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
