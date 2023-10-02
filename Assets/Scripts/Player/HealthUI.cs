using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] HitScript m_player;
    [SerializeField] List<GameObject> m_heartList;

    void FixedUpdate()
    {
        for (int i = 0; i < m_heartList.Count; i++) 
        {
            m_heartList[i].SetActive(m_player.Health > i);
        }
    }
}
