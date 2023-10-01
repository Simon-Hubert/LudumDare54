using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] GameObject m_FlameObject;
    [SerializeField] float m_turnSpeed;
    [SerializeField] float m_activationTime;
    bool m_throwerActive;

    [SerializeField] List<GameObject> m_flamesObjects;
    [SerializeField] List<GameObject> m_warningObjects;
    bool[] m_flamesActive = new bool[4];

    public bool throwerActive { get => m_throwerActive; set => m_throwerActive = value; }

    private void OnDisable() => StopAllCoroutines();

    private void FixedUpdate()
    {
        if (m_throwerActive)
        {
            transform.Rotate(new Vector3(0f, 0f, m_turnSpeed * Time.deltaTime));   
        }
    }

    public void ActivateFlames(int flames)
    {
        if (flames < 0 && flames > 15) throw new ArgumentException("CON DE GD cette valeur du lance-flamme est interdite");

        //Convertir int en binaire
        string binary = Convert.ToString(flames, 2);

        if (flames == 0) binary = "0000";
        else if (binary.Length == 1) binary = "000" + binary;
        else if (binary.Length == 2) binary = "00" + binary;
        else if (binary.Length == 3) binary = "0" + binary;

        //m_throwerActive = flames != 0;
        for(int i = 0; i < 4; i++) { m_flamesActive[i] = binary[i] == 49 ? true : false; }

        for (int i = 0; i < m_flamesObjects.Count; i++)
        {
            m_warningObjects[i].SetActive(binary[i] == 49 ? true : false);
        }
        if(flames == 0) for (int i = 0; i < m_flamesObjects.Count; i++) m_flamesObjects[i].SetActive(false);
        if (flames != 0) StartCoroutine(WaitToActivate(m_activationTime));
    }

    public void ChangeSpeed(float value) => m_turnSpeed = value;
    public void ChangeTurn(bool value) => m_throwerActive = value;
    [Button]
    void DEBUG_ACTIVE() => ActivateFlames(1);
    [Button]
    void DEBUG_DEACTIVE() => ActivateFlames(0);

    IEnumerator WaitToActivate(float m_time)
    {
        yield return new WaitForSeconds(m_time);

        for (int i = 0; i < m_flamesObjects.Count; i++) m_flamesObjects[i].SetActive(m_flamesActive[i]);
        foreach (GameObject obj in m_warningObjects) obj.SetActive(false);
    }
}
