using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_text;
    [SerializeField] string m_start = "Score : ";

    void Update()
    {
        m_text.text = m_start + GameStateManager.Instance.Score.ToString("#.00");
    }
}
