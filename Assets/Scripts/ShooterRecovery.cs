using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRecovery : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameController.Instance.ChooseRandonShooter();
        Destroy(this.gameObject);
    }
}
