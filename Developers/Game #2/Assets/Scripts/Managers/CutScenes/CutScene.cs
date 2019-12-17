using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Reflection;
using System;

[RequireComponent(typeof(PlayableDirector), typeof(BoxCollider2D))]
public class CutScene : MonoBehaviour
{
    private PlayableDirector cutscene;
    private BoxCollider2D bc;

    public bool postAction;

    public Actions action;

    public enum Actions
    {
        Test
    }

    private void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
        bc = GetComponent<BoxCollider2D>();

        if (cutscene.playOnAwake)
        {
            CutSceneManager.instance.RegisterAwakeCutScene(cutscene, OnCutSceneEnd);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!cutscene.playOnAwake) CutSceneManager.instance.PlayCutScene(cutscene, OnCutSceneEnd);
            bc.enabled = false;
        }
    }

    private void OnCutSceneEnd()
    {
        if (!postAction) return;

        Type type = typeof(PostActions);
        object instance = Activator.CreateInstance(type);

        MethodInfo method = type.GetMethod(action.ToString() + "_CutScenePostAction");

        if (method == null)
        {
            Debug.LogError("The post execution named " + action.ToString() + " does not exist!");
            return;
        }

        method.Invoke(instance, null);
    }
}
