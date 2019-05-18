using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyFX : MonoBehaviour
{
    public void AnimationEnd()
    {
        Destroy(gameObject);
    }
}
