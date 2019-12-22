using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void PauseEvent();
    public static event PauseEvent OnGamePause;
    public static event PauseEvent OnGameResume;

    [HideInInspector] public bool isGamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public void PauseGame()
    {
        if (isGamePaused) return;

        OnGamePause?.Invoke();

        isGamePaused = true;

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (!isGamePaused) return;

        OnGameResume?.Invoke();

        isGamePaused = false;

        Time.timeScale = 1;
    }
}
