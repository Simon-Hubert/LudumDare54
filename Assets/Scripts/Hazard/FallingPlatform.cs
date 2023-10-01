using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_sR;
    [SerializeField] float m_fallTime;

    bool m_isFalling = false;
    public bool IsFalling { get => m_isFalling; }

    public void StartFalling() => StartCoroutine(StartFall());
    public void ResetPlatform()
    {
        m_isFalling = false;
        m_sR.enabled = true;
        transform.tag = "Untagged";
        m_sR.color = new Color(1f, 1f, 1f);
    }

    IEnumerator StartFall()
    {
        m_isFalling = true;

        float timeElapsed = 0;

        while (timeElapsed < m_fallTime)
        {
            m_sR.color = new Color(1f, 
                                   1f - timeElapsed / m_fallTime, 
                                   1f - timeElapsed / m_fallTime, 
                                   1f - (timeElapsed / m_fallTime) / 2);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        m_sR.enabled = false;
        transform.tag = "Danger";
    }

    [Button]
    void DEBUG_StartFalling() => StartFalling();
    [Button]
    void DEBUG_Reset() => ResetPlatform();
}
