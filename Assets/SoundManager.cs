using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] GameObject m_menuSound;
    [SerializeField] GameObject m_exclamationSound;
    [SerializeField] GameObject[] m_creatureSounds;
    [SerializeField] GameObject[] m_signSounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuSound()
    {
        GameObject go = Instantiate(m_menuSound);
        Destroy(go, 2f);
    }

    public void PlayCreatureSound()
    {
        GameObject sound = m_creatureSounds[Random.Range(0, m_creatureSounds.Length)];
        GameObject go = Instantiate(sound);
        Destroy(go, 3f);
    }

    public void PlaySignSound()
    {
        GameObject sound = m_signSounds[Random.Range(0, m_signSounds.Length)];
        GameObject go = Instantiate(sound);
        Destroy(go, 3f);
    }

    public void PlayExclamationSound()
    {
        GameObject go = Instantiate(m_exclamationSound);
        Destroy(go, 2f);
    }
}
