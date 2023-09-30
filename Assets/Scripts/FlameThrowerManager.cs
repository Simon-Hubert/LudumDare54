using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] GameObject m_FlameObject;
    [SerializeField] float m_turnSpeed;
    [SerializeField] float m_flameDelay;
    bool m_flameActive;

    private void OnEnable() => StartCoroutine(WaitForFlame(m_flameDelay));
    private void OnDisable() => StopAllCoroutines();

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f,0f, m_turnSpeed * Time.deltaTime));
        m_FlameObject.SetActive(m_flameActive);
    }

    IEnumerator WaitForFlame(float m_time)
    {
        yield return new WaitForSeconds(m_time);
        m_flameActive = !m_flameActive;
        StartCoroutine(WaitForFlame(m_time));
    }
}
