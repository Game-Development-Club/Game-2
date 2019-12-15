using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager instance;
    public bool cutSceneIsPlaying;
    private bool cutSceneIsFinished;

    private PlayableDirector currentCutScene;
    private Action currentAction;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        CheckForCutSceneEnd();
    }

    public void LoadCutScene(PlayableDirector cutscene, Action onCutSceneEndOrSkip)
    {
        cutSceneIsPlaying = true;

        cutscene.Play();

        currentCutScene = cutscene;
        currentAction = onCutSceneEndOrSkip;
    }

    private void CheckForCutSceneEnd()
    {
        if (cutSceneIsPlaying)
        {
            if (currentCutScene.state != PlayState.Playing)
            {
                cutSceneIsPlaying = false;

                currentAction?.Invoke();
            }
        }
    }
}
