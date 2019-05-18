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

        m_currentMenu.alpha = 1f;
        m_currentMenu.interactable = true;
        m_currentMenu.blocksRaycasts = true;
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