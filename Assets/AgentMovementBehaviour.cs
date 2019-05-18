using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovementBehaviour : MonoBehaviour
{
    public Action OnPathEnd;
    public EDirection Direction
    {
        get;
        private set;
    }

    private Coroutine m_currentMovement = null;

    private void Start()
    {
        GameStateManager.Instance.OnGameOver += GameOver;

        //if(waypoints.Count > 0)
        //{
        //    Vector3[] path = new Vector3[waypoints.Count];
        //    for (int i = 0; i < waypoints.Count; i++)
        //    {
        //        path[i] = waypoints[i].position;
        //    }

        //    m_currentMovement = StartCoroutine(
        //        _MoveToPosition(
        //            AgentSettings.Instance.CenterTransform.position,
        //            AgentSettings.Instance.TimeToMoveFromSideToCenter,
        //            () => {
        //                Debug.Log("GO ON PATH");
        //                var ok = transform.DOPath(path, AgentSettings.Instance.TimeToMoveFromCenterToCreature, PathType.Linear, PathMode.Full3D);
        //                return;
        //            }
        //        )
        //    );
        //    //transform.DOPath(waypoints.ToArray(), AgentSettings.Instance.TimeToMoveFromCenterToCreature, PathType.CubicBezier, PathMode.Full3D);
        //    //return;
        //}

        m_currentMovement = StartCoroutine(
            _MoveToPosition(
                AgentSettings.Instance.CenterTransform.position,
                AgentSettings.Instance.TimeToMoveFromSideToCenter,
                MoveToSignDirection
            )
        );
    }

    private void OnDestroy()
    {
        m_currentMovement = null;
        GameStateManager.Instance.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        StopCoroutine(m_currentMovement);
    }

    private IEnumerator _MoveToPosition(Vector2 p_position, float p_timeToMove, Action OnMovementEnd, bool p_scaling = false)
    {
        Vector2 currentPos = transform.position;
        Vector2 startScale = transform.localScale;
        Vector2 endScale = transform.localScale * AgentSettings.Instance.ScaleFactorAtEndPath;

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / p_timeToMove;
            transform.position = Vector2.Lerp(currentPos, p_position, t);
            if(p_scaling)
                transform.localScale = Vector2.Lerp(startScale, endScale, t);
            yield return null;
        }

        OnMovementEnd?.Invoke();
    }

    private void MoveToSignDirection()
    {
        Debug.Log("Now move to sign direction");

        Vector2 finalPosition = Vector2.zero;

        Direction = AgentSettings.Instance.GetSignDirection();

        switch (Direction)
        {
            case EDirection.LEFT:
                finalPosition = AgentSettings.Instance.LeftPathTransform.position;
                break;
            case EDirection.MIDDLE:
                finalPosition = AgentSettings.Instance.MiddlePathTransform.position;
                break;
            case EDirection.RIGHT:
                finalPosition = AgentSettings.Instance.RighPathTransform.position;
                break;
        }

        m_currentMovement = StartCoroutine(
            _MoveToPosition(
                finalPosition,
                AgentSettings.Instance.TimeToMoveFromCenterToCreature,
                PathEnd,
                true
            )
        );
    }

    private void PathEnd()
    {
        OnPathEnd?.Invoke();
        Destroy(gameObject);
    }
}
