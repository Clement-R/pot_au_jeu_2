using System.Collections;
using UnityEngine;

public class TutorialTextPart : MonoBehaviour
{
    [SerializeField] private float m_timeAfterText = 0.5f;

    public bool IsDone
    {
        get;
        private set;
    } = false;

    private FadingText m_text;

    private void Awake()
    {
        m_text = GetComponent<FadingText>();
    }

    public void Play()
    {
        StartCoroutine(m_text._AnimateVertexColors());
        StartCoroutine(_SkipAfterTextDone());
    }

    private IEnumerator _SkipAfterTextDone()
    {
        while(m_text.IsPlaying)
            yield return null;

        yield return new WaitForSeconds(m_timeAfterText);

        IsDone = true;
    }
}
