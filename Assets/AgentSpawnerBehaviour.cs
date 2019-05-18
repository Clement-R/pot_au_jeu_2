using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentSpawnerBehaviour : MonoBehaviour
{
    public Action<AgentBehaviour> OnAgentSpawned;

    [SerializeField] private float m_delayBetweenSpawn = 2.5f;

    private void Start()
    {
        StartCoroutine(_GameStart());
    }

    private IEnumerator _GameStart()
    {
        while(!GameStateManager.Instance.IsGameOver)
        {
            AgentBehaviour agent = AgentSettings.Instance.Agents[Random.Range(0, AgentSettings.Instance.Agents.Count)];
            SpawnAgent(agent.gameObject);
            yield return new WaitForSeconds(m_delayBetweenSpawn);
        }
    }

    private void SpawnAgent(GameObject p_agent)
    {
        //int rand = Random.Range(0, Enum.GetNames(typeof(ESide)).Length);
        //ESide side = (ESide)rand;

        //float yRotation = side == ESide.LEFT ? 0f : 180f;
        //Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f);

        //Vector2 position;
        //if(side == ESide.LEFT)
        //    position = AgentSettings.Instance.LeftStartTransform.position;
        //else
        //    position = AgentSettings.Instance.RighStartTransform.position;

        Vector3 position = AgentSettings.Instance.LeftStartTransform.position;
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        GameObject agent = Instantiate(p_agent, position, rotation);
        OnAgentSpawned?.Invoke(agent.GetComponent<AgentBehaviour>());
    }
}

enum ESide
{
    LEFT = 0,
    RIGHT = 1
}