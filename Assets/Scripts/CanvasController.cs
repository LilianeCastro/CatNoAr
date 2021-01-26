using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _panelInGame;
    [SerializeField] private GameObject _panelGameOver;

    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private GameObject _panelDefeat;

    [SerializeField] private Text _timeElapsed;

    private void Start()
    {
        SetInGameActive();
    }

    public void SetInGameActive()
    {
        _panelInGame.SetActive(true);
        
        _panelGameOver.SetActive(false);
        _panelVictory.SetActive(false);
        _panelDefeat.SetActive(false);
    }

    public void SetGameOverVictoryActive(string time)
    {
        _panelInGame.SetActive(false);

        _panelGameOver.SetActive(true);
        _panelVictory.SetActive(true);
        _timeElapsed.text = time;
    }

    public void SetGameOverDefeatActive()
    {
        _panelInGame.SetActive(false);
        
        _panelGameOver.SetActive(true);
        _panelDefeat.SetActive(true);
    }
}
