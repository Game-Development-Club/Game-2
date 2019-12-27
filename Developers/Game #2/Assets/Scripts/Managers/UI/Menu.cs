using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Menu : MonoBehaviour
{
    protected static List<Canvas> screens;

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

        if (openOnStart) Open();
    }

    #region Screen Management

    protected void Open()
    {
        OpenScreen(startScreen);
    }

    protected static void OpenScreen(string screen)
    {
        Canvas scr = screens.Find(s => s.gameObject.name == screen);

        CloseAllScreens();

        scr.gameObject.SetActive(true);
    }

    public void OpenScreen(Canvas screen)
    {
        CloseAllScreens();

        screen.gameObject.SetActive(true);
    }

    protected static void Close()
    {
        CloseAllScreens();
    }

    private static void CloseAllScreens()
    {
        foreach (Canvas canvas in screens) canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Input Management

    public void E()
    {

    }

    #endregion
}
