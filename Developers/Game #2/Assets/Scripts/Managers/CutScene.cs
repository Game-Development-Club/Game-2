using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutScene : MonoBehaviour
{
    private PlayableDirector cutscene;

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
}
