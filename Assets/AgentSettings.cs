
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSettings : MonoBehaviour
{
    public static AgentSettings Instance;

    [Header("Base path transforms")]
    public Transform LeftStartTransform;
    public Transform RighStartTransform;
    public Transform CenterTransform;
    public float TimeToMoveFromSideToCenter;

    [Header("Creatures path transforms")]
    public Transform LeftPathTransform;
    public Transform RighPathTransform;
    public Transform MiddlePathTransform;
    public float TimeToMoveFromCenterToCreature;
    public float ScaleFactorAtEndPath;

    [Header("Agents")]
    public List<AgentBehaviour> Agents = new List<AgentBehaviour>();

    [SerializeField] private SignBehaviour m_sign;

    private void Awake()
    {
        Instance = this;
    }

    public EDirection GetSignDirection()
    {
        return m_sign.Direction;
    }
}

public enum EInfluence
{
    POSITIVE_HIGH = 0,
    POSITIVE_LOW = 1,
    NEGATIVE_HIGH = 2,
    NEGATIVE_LOW = 3
}