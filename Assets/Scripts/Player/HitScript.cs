using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    [SerializeField] GameObject m_canvasGameOver;
    [SerializeField] SpriteRenderer m_sR;
    [SerializeField] int m_invincibleSeconds;
    [SerializeField] int m_health;
    public int Health { get => m_health; }


    bool m_canBeHit;
    bool m_isdead;

    public bool IsDead {  get { return m_isdead; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Danger" && m_canBeHit)
        {
            m_canBeHit = false;

            m_health--;
            if (m_health == 0)
            {
                m_isdead = true;
                m_canvasGameOver.SetActive(true);
            }
            else
            {
                m_sR.color = new Color(1f, 1f, 1f, .5f);
                StartCoroutine(WaitToBeHit());
            }
        }
    }

    IEnumerator WaitToBeHit()
    {
        yield return new WaitForSeconds(m_invincibleSeconds);
        m_canBeHit = true;
        m_sR.color = new Color(1f, 1f, 1f, 1f);
    }
}
