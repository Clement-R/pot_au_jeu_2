using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private CanvasGroup m_pauseMenu;
    [SerializeField] private CanvasGroup m_gameoverMenu;

    private CanvasGroup m_currentMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMenu(EMenu p_menu)
    {
        switch (p_menu)
        {
            case EMenu.PAUSE:
                m_currentMenu = m_pauseMenu;
                break;
            case EMenu.GAMEOVER:
                m_currentMenu = m_gameoverMenu;
                break;
        }

        StartCoroutine(_ShowMenu(m_currentMenu));
    }

    private IEnumerator _ShowMenu(CanvasGroup p_canvas)
    {
        var t = 0f;
        var timeToMove = 0.5f;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / timeToMove;
            p_canvas.alpha = t;
            yield return new WaitForSecondsRealtime(0.033f);
        }

        p_canvas.interactable = true;
        p_canvas.blocksRaycasts = true;
    }

    public void CloseMenu()
    {
        m_currentMenu.alpha = 0f;
        m_currentMenu.interactable = false;
        m_currentMenu.blocksRaycasts = false;
    }
}

public enum EMenu
{
    PAUSE = 0,
    GAMEOVER = 1
}