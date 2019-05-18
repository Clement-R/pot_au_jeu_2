using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMovementBehaviour))]
public class AgentBehaviour : MonoBehaviour
{
    public Action<AgentBehaviour, EDirection, float> OnBeingEaten;
    public EInfluence Influence;

    private AgentMovementBehaviour m_movement;

    private void Start()
    {
        m_movement = GetComponent<AgentMovementBehaviour>();
        m_movement.OnPathEnd += GiveInfluence;
    }
    
    public void GiveInfluence()
    {
        EDirection direction = m_movement.Direction;
        InfluenceToValue infToVal = SatisfactionGaugeSettings.Instance.InfluenceToValues.Find(e => e.Influence == Influence);
        OnBeingEaten?.Invoke(this, direction, infToVal.Value);
    }
}
