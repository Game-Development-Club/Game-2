﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static List<IPauseable> pauseableObjects  = new List<IPauseable>();

    [HideInInspector] public bool isGamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene loaded, LoadSceneMode mode)
    {
        foreach (IPauseable obj in FindObjectsOfType<MonoBehaviour>().OfType<IPauseable>())
        {
            pauseableObjects.Add(obj);
        }
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
