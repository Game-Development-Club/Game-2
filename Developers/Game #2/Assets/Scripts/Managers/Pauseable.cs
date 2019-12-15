using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauseable : GameManager
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

    protected new virtual void OnGamePause()
    {

    }

    protected new virtual void OnGameResume()
    {

    }
}
