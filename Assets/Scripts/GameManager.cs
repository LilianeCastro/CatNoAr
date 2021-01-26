using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private float _minutes;
    private float _seconds;
    private bool _isThisFirstTime;

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

    public bool FirstTime
    {
        get
        {
            return PlayerPrefs.GetInt("firstTime") == 0 ? false : true;
        }
        set
        {
            _isThisFirstTime = value;
            PlayerPrefs.SetInt("firstTime", 0);
        }
    }

    public override void Init()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void UpdateScore(float newMinutes, float newSeconds)
    {
        if (newMinutes <= Minutes)
        {
            if (newSeconds < Seconds)
            {
                string score = string.Format("{0:00}:{1:00}", _minutes, _seconds);
                PlayerPrefs.SetString("hiscore", score);
            }
        }

        Minutes = newMinutes;
        Seconds = newSeconds;
    }

    public void ResetValues()
    {
        FirstTime = true;

        _minutes = 0f;
        _seconds = 0f;

        Minutes = _minutes;
        Seconds = _seconds;
        
    }

}
