using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public Action OnGameOver;
    public Action<bool> OnPause;

    public bool IsGameOver
    {
        get;
        private set;
    }

    public bool Pause
    {
        get;
        private set;
    } = false;

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
        if(!IsGameOver)
        {
            if (Gauges.Any(e => e.Gauge.Value <= 0 || e.Gauge.Value >= m_gaugeMaxValue))
                GameOver();
        }

        if (Input.GetKeyDown(KeyCode.R))
            GameOver();

        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();

        if (Input.GetKeyDown(KeyCode.O))
            RestartGame();
    }

    public void TogglePause()
    {
        if (IsGameOver)
            return;

        Pause = !Pause;
        if(Pause)
        {
            Time.timeScale = 0f;
            MenuManager.Instance.ShowMenu(EMenu.PAUSE);
        }
        else
        {
            Time.timeScale = 1f;
            MenuManager.Instance.CloseMenu();
        }

        OnPause?.Invoke(Pause);
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        IsGameOver = true;
        MenuManager.Instance.ShowMenu(EMenu.GAMEOVER);
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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