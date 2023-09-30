using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] GameObject m_FlameObject;
    [SerializeField] float m_turnSpeed;
    [SerializeField] float m_flameDelay;
    bool m_throwerActive;

    [SerializeField] List<GameObject> m_flamesObjects;
    public List<bool> m_flamesActive;

    public bool throwerActive { get => m_throwerActive; set => m_throwerActive = value; }

    private void OnEnable() => StartCoroutine(WaitForFlame(m_flameDelay));
    private void OnDisable() => StopAllCoroutines();

    private void FixedUpdate()
    {
        if (m_throwerActive)
        {
            transform.Rotate(new Vector3(0f, 0f, m_turnSpeed * Time.deltaTime));   
        }
    }

    public void ActivateFlames(bool active)
    {
        m_throwerActive = active;
        if (m_throwerActive)
        {
            for (int i = 0; i < m_flamesObjects.Count; i++)
            {
                m_flamesObjects[i].SetActive(m_flamesActive[i]);
            }
        }
        else
        {
            foreach(GameObject obj in m_flamesObjects) obj.SetActive(false);
        }
    }

    [Button]
    void DEBUG_ACTIVE() => ActivateFlames(true);
    [Button]
    void DEBUG_DEACTIVE() => ActivateFlames(false);

    IEnumerator WaitForFlame(float m_time)
    {
        yield return new WaitForSeconds(m_time);

        StartCoroutine(WaitForFlame(m_time));
    }
}
