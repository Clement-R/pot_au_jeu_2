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
    [SerializeField] private FloatingText m_floatingTextPrefab;
    [SerializeField] private Transform m_floatingTextTransform;
    [SerializeField] private GameObject m_moodUp;
    [SerializeField] private GameObject m_moodDown;

    private Coroutine m_blink = null;

    private void Start()
    {
        Value = SatisfactionGaugeSettings.Instance.MaxValue / 2f;
        UpdateGaugeUI();
        StartCoroutine(_ChangeMood());
    }

    public void ChangeInfluence(float p_value, bool p_floatingText = true)
    {
        Value = Mathf.Clamp(Value + p_value, 0f, SatisfactionGaugeSettings.Instance.MaxValue);

        if(p_floatingText)
        {
            var go = Instantiate(m_floatingTextPrefab.gameObject, m_floatingTextTransform.position, Quaternion.identity);
            go.GetComponent<FloatingText>().Init(p_value.ToString());
        }
    }

    private void Update()
    {
        UpdateGaugeUI();

        if (GameStateManager.Instance.IsGameOver || GameStateManager.Instance.Pause)
            return;

        switch (m_mood)
        {
            case ECreatureMood.GOOD:
                ChangeInfluence(0.025f, false);
                break;
            case ECreatureMood.BAD:
                ChangeInfluence(-0.025f, false);
                break;
        }
    }

    private void UpdateGaugeUI()
    {
        m_gaugeImage.fillAmount = Value / SatisfactionGaugeSettings.Instance.MaxValue;
    }

    private IEnumerator _ChangeMood()
    {
        while(!GameStateManager.Instance.IsGameOver)
        {
            while (GameStateManager.Instance.Pause)
                yield return null;

            m_mood = (ECreatureMood)Random.Range(0, Enum.GetNames(typeof(ECreatureMood)).Length);

            if (m_blink != null)
            {
                StopCoroutine(m_blink);
                m_blink = null;
            }

            switch (m_mood)
            {
                case ECreatureMood.GOOD:
                    m_moodDown.SetActive(false);
                    m_blink = StartCoroutine(_Blink(m_moodUp));
                    break;
                case ECreatureMood.BAD:
                    m_moodUp.SetActive(false);
                    m_blink = StartCoroutine(_Blink(m_moodDown));
                    break;
            }

            yield return new WaitForSeconds(6f);
        }
    }

    private IEnumerator _Blink(GameObject p_go)
    {
        while(true)
        {
            while (GameStateManager.Instance.Pause || GameStateManager.Instance.IsGameOver)
                yield return null;

            p_go.SetActive(true);
            yield return new WaitForSeconds(0.33f);
            p_go.SetActive(false);
            yield return new WaitForSeconds(0.33f);
        }
    }
}

public enum ECreatureMood
{
    GOOD = 0,
    BAD = 1
}