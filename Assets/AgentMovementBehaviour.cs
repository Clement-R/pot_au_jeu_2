using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class AgentMovementBehaviour : MonoBehaviour
{
    public Action OnPathEnd;
    public EDirection Direction
    {
        get;
        private set;
    }

    [SerializeField] GameObject m_exclamation;

    private Tween m_currentMovement = null;
    private Vector3 m_endScale;

    private void Start()
    {
        GameStateManager.Instance.OnGameOver += GameOver;
        GameStateManager.Instance.OnPause += Pause;

        m_currentMovement = transform.DOPath(AgentSettings.Instance.PathToCenter,
                                             AgentSettings.Instance.TimeToMoveFromSideToCenter,
                                             PathType.Linear)
                                     .OnComplete(MoveToSignDirection)
                                     .SetEase(Ease.Linear);

        if (GameStateManager.Instance.Pause)
            Pause(true);
    }

    private void Pause(bool p_pause)
    {
        m_currentMovement.TogglePause();
    }

    private void OnDestroy()
    {
        m_currentMovement = null;
        GameStateManager.Instance.OnGameOver -= GameOver;
        GameStateManager.Instance.OnPause -= Pause;
    }

    private void GameOver()
    {
        m_currentMovement.Kill();
    }

    private IEnumerator _ScaleAlongJourney(Tween p_tween)
    {
        Vector2 startScale = transform.localScale;
        m_endScale = transform.localScale * AgentSettings.Instance.ScaleFactorAtEndPath;

        float t = 0f;
        float duration = p_tween.Duration();

        while (t < duration)
        {
            transform.localScale = Vector2.Lerp(startScale, m_endScale, t / duration);
            yield return null;
            t += Time.deltaTime;
        }

        transform.localScale = m_endScale;
    }

    private IEnumerator _ShowExclamation()
    {
        SoundManager.Instance.PlayExclamationSound();
        m_exclamation.SetActive(true);
        yield return new WaitForSeconds(AgentSettings.Instance.ExclamationDuration);
        m_exclamation.SetActive(false);
    }

    private void MoveToSignDirection()
    {
        Vector3[] path = new Vector3[0];

        Direction = AgentSettings.Instance.GetSignDirection();

        StartCoroutine(_ShowExclamation());

        switch (Direction)
        {
            case EDirection.LEFT:
                path = AgentSettings.Instance.PathToLeft;
                break;
            case EDirection.MIDDLE:
                path = AgentSettings.Instance.PathToMiddle;
                break;
            case EDirection.RIGHT:
                path = AgentSettings.Instance.PathToRight;
                break;
        }

        m_currentMovement = transform.DOPath(path,
                                             AgentSettings.Instance.TimeToMoveFromCenterToCreature,
                                             PathType.Linear)
                                     .OnComplete(PathEnd)
                                     .SetEase(Ease.Linear);
        StartCoroutine(_ScaleAlongJourney(m_currentMovement));
    }

    private void PathEnd()
    {
        OnPathEnd?.Invoke();
        Destroy(gameObject, 0.2f);
    }
}
