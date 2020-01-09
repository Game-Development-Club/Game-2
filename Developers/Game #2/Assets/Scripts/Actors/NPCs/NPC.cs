using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("General Settings")]
    public float interactRadius = 1.0f;

    [Header("Dialog Settings")]
    public TextAsset dialogSequence;
    public bool loopDialog;
    public float dialogHeight;
    public bool postAction;
    public Actions action;

    private CircleCollider2D provokeRadius;

    private bool isSpeaking;

    public enum Actions
    {
        None, Test
    }

    private void Start()
    {
        if (gameObject.GetComponent<CircleCollider2D>() == null)
        {
            provokeRadius = gameObject.AddComponent<CircleCollider2D>();
            provokeRadius.isTrigger = true;
            provokeRadius.radius = interactRadius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isSpeaking)
        {
            if (!loopDialog) provokeRadius.enabled = false;

            Vector3 pos = transform.position + Vector3.up * dialogHeight;

            isSpeaking = true;

            DialogManager.instance.StartNPCDialogSequence(pos, dialogSequence, () =>
            {
                isSpeaking = false;
                if (action != Actions.None) Invoke(action.ToString(), 0f);
            });
        }
    }

    #region Post Actions

    public void Test()
    {
        Debug.Log("Success");
    }

    #endregion
}
