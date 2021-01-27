using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private float _minutes;
    private float _seconds;
    private string _highscore;
    private bool _isNewHighScore;

    private float Minutes
    {
        get 
        {
            return PlayerPrefs.GetFloat("minutes");
        }
        set
        {
            _minutes = value;
            PlayerPrefs.SetFloat("minutes", _minutes);
        }
    }

    private float Seconds
    {
        get 
        {
            return PlayerPrefs.GetFloat("seconds");
        }
        set
        {
            _seconds = value;
            PlayerPrefs.SetFloat("seconds", _seconds);
        }
    }

    public string Highscore
    {
        get
        {
            return PlayerPrefs.GetString("hi-score");
        }
        set
        {
            _highscore = value;
            PlayerPrefs.SetString("hi-score", _highscore);
        }
    }

    public bool IsNewHighScore
    {
        get
        {
            return _isNewHighScore;
        }
    }

    public override void Init()
    {
        DontDestroyOnLoad(this.gameObject);

        //ResetValues();
        _minutes = Minutes;
        _seconds = Seconds;
        _highscore = Highscore;
        
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void UpdateScore(float newMinutes, float newSeconds)
    {
        string[] highscore = Highscore.Split(':');

        if (newMinutes <= float.Parse(highscore[0]))
        {
            if (newSeconds < float.Parse(highscore[1]))
            {
                _isNewHighScore = true;
                string score = string.Format("{0:00}:{1:00}", newMinutes, newSeconds);
                Highscore = score;                
            }
            else
            {
                _isNewHighScore = false;
            }
        }

        Minutes = newMinutes;
        Seconds = newSeconds;
    }

    public string BestScore()
    {
        return Highscore;
    }

    public void ResetValues()
    {
        _minutes = 59f;
        _seconds = 59f;
        _highscore = "59:59";

        Minutes = _minutes;
        Seconds = _seconds;
        Highscore = _highscore;
        
    }

}
