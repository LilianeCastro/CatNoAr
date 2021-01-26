using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] private CanvasController _canvasController;

    [Header("Shooter")]
    [SerializeField] private Shooter _iceCreamShooter = default;
    [SerializeField] private Shooter _orbShooter = default;

    [SerializeField] private GameObject _shooterRecovery = default;

    [Header("Scores")]
    [SerializeField] private Image _imgScorePlayer = default;
    [SerializeField] private Image _imgScoreEnemy = default;
    [SerializeField] private Image[] _numberScore = default;

    [SerializeField] private Text _timer;
    private float _minutes;
    private float _seconds;
    private float _timeRemaining = 0;

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
            if (_scorePlayer <= 0)
            {
                _gameOver = true;
                _canvasController.SetGameOverDefeatActive();
            }
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

            if (_scoreEnemy <= 0)
            {
                _gameOver = true;

                string score = string.Format("{0:00}:{1:00}", _minutes, _seconds);
                _canvasController.SetGameOverVictoryActive(score);

                PlayerPrefs.SetFloat("minutes", _minutes);
                PlayerPrefs.SetFloat("seconds", _seconds);
            }
        }
    }

    private bool _gameOver = false;
    public bool GameOver
    {
        get
        {
            return _gameOver;
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

    private void Update()
    {
        if (_gameOver) { return ; }
        _timeRemaining += Time.deltaTime;

        _minutes = Mathf.FloorToInt(_timeRemaining / 60); 
        _seconds = Mathf.FloorToInt(_timeRemaining % 60);

        _timer.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}
