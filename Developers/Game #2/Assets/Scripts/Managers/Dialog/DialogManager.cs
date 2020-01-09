using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    private Dictionary<GameObject, Vector3> activeTexts;

    [Header("Dialog Assets")]
    public GameObject canvas;
    public GameObject textContainer;

    [Header("Settings")]
    public float typeSpeed = 0.02f;

    private GameObject canvasInstance;

    private void Start()
    {
        activeTexts = new Dictionary<GameObject, Vector3>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LateUpdate()
    {
        foreach (KeyValuePair<GameObject, Vector3> pair in activeTexts)
        {
            pair.Key.transform.position = Camera.main.WorldToScreenPoint(pair.Value);
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

    public void StartNPCDialogSequence(Vector3 triggerPosition, TextAsset dialog, Action onSpeakingFinish)
    {
        if (dialog == null)
        {
            Debug.LogError("Dialog sequence with the name of " + dialog + "was not found!");
            return;
        }

        string fixedText = dialog.text.Replace("\n", string.Empty);

        StartCoroutine(Type(CreateTextContainer(triggerPosition), fixedText, onSpeakingFinish));
    }

    private TextMeshProUGUI CreateTextContainer(Vector3 triggerPosition)
    {
        GameObject textObject = Instantiate(textContainer, canvasInstance.transform);
        activeTexts.Add(textObject, triggerPosition);

        textObject.transform.position = Camera.main.WorldToScreenPoint(triggerPosition);
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
