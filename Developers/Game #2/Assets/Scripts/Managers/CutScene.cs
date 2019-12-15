using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutScene : Pauseable
{
    private PlayableDirector cutscene;

    private void Start()
    {
        Debug.Log("Hi!");
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CutSceneManager.instance.LoadCutScene(cutscene, OnCutSceneEnd);
        }
    }

    protected virtual void OnCutSceneEnd()
    {

    }

    protected override void OnGamePause()
    {
        base.OnGamePause();
        Debug.Log("This cutscene was paused!");
    }

    protected override void OnGameResume()
    {
        base.OnGamePause();
        Debug.Log("This cutscene was paused!");
    }
}
