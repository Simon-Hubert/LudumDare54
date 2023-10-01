using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField] GameObject m_canvasGameOver;
    bool m_isdead;

    public bool IsDead {  get { return m_isdead; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Danger")
        {
            m_isdead = true;
            m_canvasGameOver.SetActive(true);
        }
    }
}
