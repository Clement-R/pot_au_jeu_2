﻿using System;
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

    private void Start()
    {
        StartCoroutine(
            _MoveToPosition(
                AgentSettings.Instance.CenterTransform.position,
                AgentSettings.Instance.TimeToMoveFromSideToCenter,
                MoveToSignDirection
            )
        );
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

        StartCoroutine(
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