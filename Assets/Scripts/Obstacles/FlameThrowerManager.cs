using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] GameObject m_FlameObject;
    [SerializeField] float m_turnSpeed;
    [SerializeField] float m_flameDelay;
    [SerializeField] float m_activationTime;
    bool m_throwerActive;

    [SerializeField] List<GameObject> m_flamesObjects;
    [SerializeField] List<GameObject> m_warningObjects;
    public List<bool> m_flamesActive;

    public bool throwerActive { get => m_throwerActive; set => m_throwerActive = value; }

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

        if (active)
        {
            m_throwerActive = true;
            for (int i = 0; i < m_flamesObjects.Count; i++) m_warningObjects[i].SetActive((m_flamesActive[i]));
            StartCoroutine(WaitToActivate(m_activationTime));
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

    IEnumerator WaitToActivate(float m_time)
    {
        yield return new WaitForSeconds(m_time);

        for (int i = 0; i < m_flamesObjects.Count; i++) m_flamesObjects[i].SetActive(m_flamesActive[i]);
        foreach (GameObject obj in m_warningObjects) obj.SetActive(false);
    }
}
