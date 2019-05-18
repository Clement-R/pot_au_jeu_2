using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Action OnGameOver;

    [SerializeField] private List<DirectionGauge> Gauges;
    [SerializeField] private AgentSpawnerBehaviour Spawner;

    private float m_gaugeMaxValue;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        m_gaugeMaxValue = SatisfactionGaugeSettings.Instance.MaxValue;
        Spawner.OnAgentSpawned += AgentSpawned;
    }

    private void Update()
    {
        if (Gauges.Any(e => e.Gauge.Value <= 0 || e.Gauge.Value > m_gaugeMaxValue))
            Debug.Log("Game over");
    }

    private void AgentSpawned(AgentBehaviour p_agent)
    {
        p_agent.OnBeingEaten += UpdateGauge;
    }

    private void UpdateGauge(AgentBehaviour p_agent, EDirection p_direction, float p_value)
    {
        DirectionGauge directionGauge = Gauges.Find(e => e.Direction == p_direction);
        directionGauge.Gauge.ChangeInfluence(p_value);
        p_agent.OnBeingEaten -= UpdateGauge;
    }
}

[System.Serializable]
public class DirectionGauge
{
    public EDirection Direction;
    public SatisfactionGaugeBehaviour Gauge;
}