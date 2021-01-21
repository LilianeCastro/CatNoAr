using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [Header("Scores")]
    [SerializeField] private Image _imgScorePlayer = default;
    [SerializeField] private Image _imgScoreEnemy = default;
    [SerializeField] private Image[] _numberScore = default;

    [Header("Progress to Shoot")]
    [SerializeField] private Slider _progressReadyIceCream = default;
    [SerializeField] private Slider _progressReadyOrb = default;

    private int _scorePlayer;

    public int ScorePlayer {
        get
        {
            return _scorePlayer;
        }
        set
        {
            _scorePlayer += value;
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
        }
    }

    public override void Init()
    {
        print("Game");
    }

}
