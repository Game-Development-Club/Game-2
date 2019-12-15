using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void PauseEvent();
    protected static event PauseEvent OnGamePause;
    protected static event PauseEvent OnGameResume;

    public bool isGamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public void PauseGame()
    {
        OnGamePause?.Invoke();

        isGamePaused = true;

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        OnGameResume?.Invoke();

        isGamePaused = false;

        Time.timeScale = 1;
    }
}
