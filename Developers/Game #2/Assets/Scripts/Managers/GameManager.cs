using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void PauseEvent();
    protected static event PauseEvent OnGamePause;
    protected static event PauseEvent OnGameResume;

    private void Start()
    {
        if (instance == null) instance = this;

        Debug.Log("HI");
    }

    public void PauseGame()
    {
        OnGamePause?.Invoke();

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        OnGameResume?.Invoke();

        Time.timeScale = 1;
    }
}
