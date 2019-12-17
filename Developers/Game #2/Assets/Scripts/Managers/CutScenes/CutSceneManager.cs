using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager instance;
    [HideInInspector] public bool cutSceneIsPlaying;
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

    public void PlayCutScene(PlayableDirector cutscene, Action onCutSceneEndOrSkip)
    {
        if (cutSceneIsPlaying) return;
        cutSceneIsPlaying = true;

        cutscene.Play();

        currentCutScene = cutscene;
        currentAction = onCutSceneEndOrSkip;
    }

    public void RegisterAwakeCutScene(PlayableDirector cutscene, Action onCutSceneEndOrSkip)
    {
        if (cutSceneIsPlaying) return;

        cutSceneIsPlaying = true;

        currentCutScene = cutscene;
        currentAction = onCutSceneEndOrSkip;
    }

    public void SkipCutScene()
    {
        cutSceneIsPlaying = false;

        //Implement fade transition here

        currentAction?.Invoke();
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
