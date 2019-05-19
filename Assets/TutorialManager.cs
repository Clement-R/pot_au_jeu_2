using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_tutorialParts = new List<GameObject>();
    private int m_index = 0;

    void Start()
    {
        foreach (var part in m_tutorialParts)
        {
            part.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            StartCoroutine(_DoTutorial());
    }

    private IEnumerator _DoTutorial()
    {
        while(m_index < m_tutorialParts.Count)
        {
            TutorialTextPart m_part = m_tutorialParts[m_index].GetComponent<TutorialTextPart>();

            m_tutorialParts[m_index].gameObject.SetActive(true);
            m_part.Play();
            while (!m_part.IsDone)
                yield return null;

            m_tutorialParts[m_index].gameObject.SetActive(false);
            m_index++;
        }
    }
}