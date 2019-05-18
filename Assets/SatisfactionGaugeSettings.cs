using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionGaugeSettings : MonoBehaviour
{
    public static SatisfactionGaugeSettings Instance;

    [Header("Base path transforms")]
    public float MaxValue;

    public List<InfluenceToValue> InfluenceToValues = new List<InfluenceToValue>();

    private void Awake()
    {
        Instance = this;
    }
}

[System.Serializable]
public class InfluenceToValue
{
    public EInfluence Influence;
    public float Value;
}