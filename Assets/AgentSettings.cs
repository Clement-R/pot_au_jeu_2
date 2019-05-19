
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSettings : MonoBehaviour
{
    public static AgentSettings Instance;

    [Header("Base path transforms")]
    [HideInInspector] public Vector3[] PathToCenter;
    public float TimeToMoveFromSideToCenter;

    [SerializeField] Transform[] TransformPathToCenter;

    [Header("Creatures path transforms")]
    [HideInInspector] public Vector3[] PathToLeft;
    [SerializeField] Transform[] TransformPathToLeft;
    [HideInInspector] public Vector3[] PathToMiddle;
    [SerializeField] Transform[] TransformPathToMiddle;
    [HideInInspector] public Vector3[] PathToRight;
    [SerializeField] Transform[] TransformPathToRight;
    public float TimeToMoveFromCenterToCreature;
    public float ScaleFactorAtEndPath;
    public float ExclamationDuration = 0.5f;

    [Header("Agents")]
    public List<AgentBehaviour> Agents = new List<AgentBehaviour>();

    [SerializeField] private SignBehaviour m_sign;

    private void Awake()
    {
        Instance = this;

        PathToCenter = TransformPathToVectorPath(TransformPathToCenter);
        PathToLeft = TransformPathToVectorPath(TransformPathToLeft);
        PathToMiddle = TransformPathToVectorPath(TransformPathToMiddle);
        PathToRight = TransformPathToVectorPath(TransformPathToRight);
    }

    public EDirection GetSignDirection()
    {
        return m_sign.Direction;
    }

    private Vector3[] TransformPathToVectorPath(Transform[] p_transformPath)
    {
        Vector3[] path = new Vector3[p_transformPath.Length];
        for (int i = 0; i < p_transformPath.Length; i++)
        {
            path[i] = p_transformPath[i].position;
        }

        return path;
    }
}

public enum EInfluence
{
    POSITIVE_HIGH = 0,
    POSITIVE_LOW = 1,
    NEGATIVE_HIGH = 2,
    NEGATIVE_LOW = 3
}