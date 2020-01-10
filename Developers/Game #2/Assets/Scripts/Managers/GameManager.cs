using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static List<IPauseable> pauseableObjects;

    [HideInInspector] public bool isGamePaused;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        pauseableObjects = new List<IPauseable>();

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

        AudioManager.instance.PauseAll();

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

        AudioManager.instance.UnPauseAll();

        isGamePaused = false;

        Time.timeScale = 1;
    }
}
