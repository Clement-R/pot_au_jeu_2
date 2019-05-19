using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_credits;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ShowCredits()
    {
        StartCoroutine(_ShowCredits());
    }

    public void HideCredits()
    {
        StartCoroutine(_HideCredits());
    }

    private IEnumerator _ShowCredits()
    {
        var t = 0f;
        var timeToMove = 0.5f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            m_credits.alpha = t;
            yield return null;
        }

        m_credits.interactable = true;
        m_credits.blocksRaycasts = true;
    }

    private IEnumerator _HideCredits()
    {
        m_credits.interactable = false;
        m_credits.blocksRaycasts = false;

        var t = 0f;
        var timeToMove = 0.5f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            m_credits.alpha = 1 - t;
            yield return null;
        }
    }
}
