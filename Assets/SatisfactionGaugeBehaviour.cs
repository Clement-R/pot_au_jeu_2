using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SatisfactionGaugeBehaviour : MonoBehaviour
{
    public float Value
    {
        get;
        private set;
    }

    [SerializeField] private Image m_gaugeImage;
    [SerializeField] private ECreatureMood m_mood;

    private void Start()
    {
        Value = SatisfactionGaugeSettings.Instance.MaxValue / 2f;
        UpdateGaugeUI();
        StartCoroutine(_ChangeMood());
    }

    public void ChangeInfluence(float p_value)
    {
        Value = Mathf.Clamp(Value + p_value, 0f, SatisfactionGaugeSettings.Instance.MaxValue);
    }

    private void Update()
    {
        if (GameStateManager.Instance.IsGameOver)
            return;

        switch (m_mood)
        {
            case ECreatureMood.GOOD:
                ChangeInfluence(0.05f);
                break;
            case ECreatureMood.BAD:
                ChangeInfluence(-0.05f);
                break;
        }

        UpdateGaugeUI();
    }

    private void UpdateGaugeUI()
    {
        m_gaugeImage.fillAmount = Value / SatisfactionGaugeSettings.Instance.MaxValue;
    }

    private IEnumerator _ChangeMood()
    {
        while(!GameStateManager.Instance.IsGameOver)
        {
            m_mood = (ECreatureMood)Random.Range(0, Enum.GetNames(typeof(ECreatureMood)).Length);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

public enum ECreatureMood
{
    NEUTRAL = 0,
    GOOD = 1,
    BAD = 2
}