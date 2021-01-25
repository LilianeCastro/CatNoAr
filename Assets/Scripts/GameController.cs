using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [Header("Shooter")]
    [SerializeField] private Shooter _iceCreamShooter = default;
    [SerializeField] private Shooter _orbShooter = default;

    [SerializeField] private GameObject _shooterRecovery = default;

    [Header("Scores")]
    [SerializeField] private Image _imgScorePlayer = default;
    [SerializeField] private Image _imgScoreEnemy = default;
    [SerializeField] private Image[] _numberScore = default;

    private int _scorePlayer = 7;
    public int ScorePlayer {
        get
        {
            return _scorePlayer;
        }
        set
        {
            _scorePlayer += value;
            _imgScorePlayer.sprite = _numberScore[_scorePlayer].sprite;
        }
    }

    private int _scoreEnemy = 3;
    public int ScoreEnemy {
        get 
        {
            return _scoreEnemy;
        }
        set 
        { 
            _scoreEnemy += value;
            _imgScoreEnemy.sprite = _numberScore[_scoreEnemy].sprite; 
        }
    }

    public override void Init()
    {
        ChooseRandonShooter();
    }

    public void SpawnShootRecovery()
    {
        Instantiate(_shooterRecovery, transform.position, Quaternion.identity);
    }

    public void ChooseRandonShooter()
    {
        int randonMove = Random.Range(0, 100);
        if (randonMove > 50)
        {
            _iceCreamShooter.SetCanShoot(true);
        }
        else
        {
            _orbShooter.SetCanShoot(true);
        }
    }
}
