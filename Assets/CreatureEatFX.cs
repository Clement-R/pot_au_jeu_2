using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEatFX : MonoBehaviour
{
    [SerializeField] private AgentSpawnerBehaviour m_spawner;
    [SerializeField] private EDirection m_direction;
    [SerializeField] private GameObject m_fxPrefab;
    [SerializeField] private Transform m_fxTransform;

    private void Start()
    {
        m_spawner.OnAgentSpawned += AgentSpawned;
    }

    private void AgentSpawned(AgentBehaviour p_agent)
    {
        p_agent.OnBeingEaten += PlayFx;
    }

    private void PlayFx(AgentBehaviour p_agent, EDirection p_direction, float p_value)
    {
        if(p_direction == m_direction)
        {
            Instantiate(m_fxPrefab, m_fxTransform.position, Quaternion.identity);
        }
        p_agent.OnBeingEaten -= PlayFx;
    }
}
