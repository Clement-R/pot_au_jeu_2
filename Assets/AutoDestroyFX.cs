using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyFX : MonoBehaviour
{
    [SerializeField] bool m_randomRotation = false;

    private void Start()
    {
        if (m_randomRotation)
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }

    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}
