using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRecovery : MonoBehaviour
{
    private AudioSource _recoveryAS;

    private void Start()
    {
        _recoveryAS = GetComponent<AudioSource>();
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(0.25f);
        _recoveryAS.Play();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameController.Instance.ChooseRandonShooter();
        Destroy(this.gameObject);
    }
}
