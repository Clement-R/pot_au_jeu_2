using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentSpawnerBehaviour : MonoBehaviour
{
    public Action<AgentBehaviour> OnAgentSpawned;

    [SerializeField] private float m_delayBetweenSpawn = 2.5f;
    [SerializeField] private AnimationCurve m_difficultyCurve;

    private float m_lastSpawn = 0f;
    private Coroutine m_routine = null;
    private float m_gameTime = 0f;

    private void Start()
    {
        GameStateManager.Instance.OnPause += Toggle;
        Toggle(GameStateManager.Instance.Pause);
    }

    private void Toggle(bool p_pause)
    {
        if (p_pause)
        {
            if (m_routine != null)
            {
                StopCoroutine(m_routine);
                m_routine = null;
            }
        }
        else
        {
            if(m_routine == null)
                m_routine = StartCoroutine(_GameStart());
        }
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnPause -= Toggle;
    }

    private IEnumerator _GameStart()
    {
        while(!GameStateManager.Instance.IsGameOver)
        {
            int rand = Random.Range(0, AgentSettings.Instance.Agents.Count);
            AgentBehaviour agent = AgentSettings.Instance.Agents[rand];
            SpawnAgent(agent.gameObject);

            yield return new WaitForSeconds(m_difficultyCurve.Evaluate(m_gameTime));
        }
    }

    private void Update()
    {
        if(!GameStateManager.Instance.IsGameOver && !GameStateManager.Instance.Pause)
            m_gameTime += Time.deltaTime;
    }

    private void SpawnAgent(GameObject p_agent)
    {
        Vector3 position = AgentSettings.Instance.PathToCenter[0];
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
        GameObject agent = Instantiate(p_agent, position, rotation);
        OnAgentSpawned?.Invoke(agent.GetComponent<AgentBehaviour>());
        m_lastSpawn = Time.time;
    }
}

enum ESide
{
    LEFT = 0,
    RIGHT = 1
}