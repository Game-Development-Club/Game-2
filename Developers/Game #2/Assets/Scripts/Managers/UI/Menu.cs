using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public static List<Canvas> screens;

    public Canvas startScreen;
    public bool openOnStart;

    protected virtual void Start()
    {
        screens = new List<Canvas>();

        foreach (Canvas canvas in gameObject.GetComponentsInChildren<Canvas>())
        {
            screens.Add(canvas);
            canvas.gameObject.SetActive(false);
        }

        if (openOnStart) OpenScreen(startScreen);
    }

    public void OpenScreen(Canvas screen)
    {
        CloseAllScreens();

        screen.gameObject.SetActive(true);
    }

    public void Close()
    {
        CloseAllScreens();
    }

    private void CloseAllScreens()
    {
        foreach (Canvas canvas in screens) canvas.gameObject.SetActive(false);
    }
}
