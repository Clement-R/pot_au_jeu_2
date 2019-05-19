using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private FadingText m_partOne;
    [SerializeField] private FadingText m_partTwo;
    [SerializeField] private CanvasGroup m_values;
    [SerializeField] private CanvasGroup m_background;
    [SerializeField] private CanvasGroup m_start;
    [SerializeField] private CanvasGroup m_skip;

    private const string K_PP_TUTORIAL = "TutorialDone";

    private Coroutine m_tuto;

    void Start()
    {
        GameStateManager.Instance.FakePause();

        if (PlayerPrefs.HasKey(K_PP_TUTORIAL))
        {
            if (PlayerPrefs.GetInt(K_PP_TUTORIAL) == 0)
                m_tuto = StartCoroutine(_DoTutorial());
            else
                GameStateManager.Instance.FakePause();
        }
        else
            m_tuto = StartCoroutine(_DoTutorial());

        m_partOne.gameObject.SetActive(false);
        m_partTwo.gameObject.SetActive(false);
    }

    private IEnumerator _DoTutorial()
    {
        var t = 0f;
        var timeToMove = 0.5f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            m_background.alpha = t;
            m_skip.alpha = t;
            yield return null;
        }

        m_background.blocksRaycasts = true;
        m_background.interactable = true;

        m_partOne.gameObject.SetActive(true);
        yield return m_partOne._AnimateVertexColors();

        m_partTwo.gameObject.SetActive(true);
        yield return m_partTwo._AnimateVertexColors();

        t = 0f;
        timeToMove = 0.5f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            m_values.alpha = t;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        t = 0f;
        timeToMove = 0.5f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            m_start.alpha = t;
            yield return null;
        }
    }

    public void Skip()
    {
        StopCoroutine(m_tuto);
        Launch();
    }

    public void Launch()
    {
        StartCoroutine(_Launch());
    }

    private IEnumerator _Launch()
    {
        float t = 0f;
        float timeToMove = 0.5f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            m_background.alpha = 1f - t;
            yield return null;
        }

        PlayerPrefs.SetInt(K_PP_TUTORIAL, 1);
        PlayerPrefs.Save();
        GameStateManager.Instance.FakePause();

        m_background.blocksRaycasts = false;
        m_background.interactable = false;
    }
}