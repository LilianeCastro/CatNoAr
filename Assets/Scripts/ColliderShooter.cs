using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShooter : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _shooter.PlayerInAreaToShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _shooter.PlayerInAreaToShoot = false;
            _shooter.transform.rotation = Quaternion.Euler(_shooter.InitialRotation.x, _shooter.InitialRotation.y, _shooter.InitialRotation.z);
        }
    }
}
