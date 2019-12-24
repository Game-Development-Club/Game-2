using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static List<IPauseable> pauseableObjects;

    [HideInInspector] public bool isGamePaused;

    private void Start()
    {
        if (instance == null) instance = this;

        pauseableObjects = new List<IPauseable>();
    }

    private void Update()
    {

    }

    public static void RegisterPauseableObject(IPauseable obj)
    {
        pauseableObjects.Add(obj);
    }

    public void PauseGame()
    {
        if (isGamePaused) return;

        foreach (IPauseable pauseableObj in pauseableObjects)
        {
            pauseableObj.OnGamePause();
        }

        isGamePaused = true;

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (!isGamePaused) return;

        foreach (IPauseable pauseableObj in pauseableObjects)
        {
            pauseableObj.OnGameResume();
        }

        isGamePaused = false;

        Time.timeScale = 1;
    }
}
