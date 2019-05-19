using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeMusic : MonoBehaviour
{
    public static MainThemeMusic Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
