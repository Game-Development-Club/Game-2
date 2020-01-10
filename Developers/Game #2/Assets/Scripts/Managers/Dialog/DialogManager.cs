using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private Dictionary<GameObject, Tuple<Transform, float>> activeTexts;

    [Header("Dialog Assets")]
    public GameObject canvas;
    public GameObject textContainer;

    [Header("Settings")]
    public float typeSpeed = 0.02f;

    private GameObject canvasInstance;

    private void Start()
    {
        activeTexts = new Dictionary<GameObject, Tuple<Transform, float>>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LateUpdate()
    {
        foreach (KeyValuePair<GameObject, Tuple<Transform, float>> pair in activeTexts)
        {
            Vector3 pos = pair.Value.Item1.position;
            pos.y += pair.Value.Item2;

            pair.Key.transform.position = Camera.main.WorldToScreenPoint(pos);
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
        canvasInstance = Instantiate(canvas);
    }

    public void StartNPCDialogSequence(Transform triggerPosition, float dialogHeight, TextAsset dialog, Action onSpeakingFinish)
    {
        if (dialog == null)
        {
            Debug.LogError("Dialog sequence with the name of " + dialog + "was not found!");
            return;
        }

        string fixedText = dialog.text.Replace("\n", string.Empty);

        StartCoroutine(Type(CreateTextContainer(triggerPosition, dialogHeight), fixedText, onSpeakingFinish));
    }

    private TextMeshProUGUI CreateTextContainer(Transform triggerPosition, float dialogHeight)
    {
        GameObject textObject = Instantiate(textContainer, canvasInstance.transform);
        activeTexts.Add(textObject, new Tuple<Transform, float>(triggerPosition, dialogHeight));

        return textObject.GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator Type(TextMeshProUGUI container, string text, Action onSpeakingFinish)
    {
        string txt;

        foreach (string line in text.Split('/'))
        {
            txt = "";

            foreach (char c in line.ToCharArray())
            {
                if (c == '/') continue;

                txt += c;
                container.SetText(txt);
                yield return new WaitForSeconds(typeSpeed);
            }

            yield return new WaitForSeconds(1f);
        }

        activeTexts.Remove(container.gameObject);
        Destroy(container.gameObject);

        onSpeakingFinish?.Invoke();
    }
}
