using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PauseableObject : GameManager
{
    protected virtual void OnEnable()
    {
        GameManager.OnGamePause += OnGamePause;
        GameManager.OnGameResume += OnGameResume;
    }

    protected virtual void OnDisable()
    {
        GameManager.OnGamePause -= OnGamePause;
        GameManager.OnGameResume -= OnGameResume;
    }

    protected abstract new void OnGamePause();

    protected abstract new void OnGameResume();
}