using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBehaviour : MonoBehaviour
{
    public EDirection Direction
    {
        get;
        private set;
    }

    [SerializeField] private KeyCode m_rightKey;
    [SerializeField] private KeyCode m_middleKey;
    [SerializeField] private KeyCode m_leftKey;

    [SerializeField] private Sprite m_leftSign;
    [SerializeField] private Sprite m_middleSign;
    [SerializeField] private Sprite m_rightSign;

    [SerializeField] private SpriteRenderer m_sign;

    private void Start()
    {
        ChangeSign(EDirection.MIDDLE);
    }

    void Update()
    {
        if (Input.GetKeyDown(m_rightKey))
            ChangeSign(EDirection.RIGHT);
        else if (Input.GetKeyDown(m_middleKey))
            ChangeSign(EDirection.MIDDLE);
        else if (Input.GetKeyDown(m_leftKey))
            ChangeSign(EDirection.LEFT);
    }

    private void ChangeSign(EDirection p_direction)
    {
        Direction = p_direction;

        switch (p_direction)
        {
            case EDirection.LEFT:
                m_sign.sprite = m_leftSign;
                break;
            case EDirection.MIDDLE:
                m_sign.sprite = m_middleSign;
                break;
            case EDirection.RIGHT:
                m_sign.sprite = m_rightSign;
                break;
        }
    }
}

public enum EDirection
{
    LEFT = 0,
    MIDDLE = 1,
    RIGHT = 2
}