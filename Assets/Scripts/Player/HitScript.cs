using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitScript : MonoBehaviour
{
    [SerializeField] GameObject m_canvasGameOver;
    [SerializeField] SpriteRenderer m_sR;
    [SerializeField] int m_invincibleSeconds;
    [SerializeField] float m_blinkTime;
    [SerializeField] int m_health;

    // Fields de Simon
    [SerializeField] UnityEvent _onHit;
    [SerializeField] UnityEvent _onDeath;

    public int Health { get => m_health; }


    bool m_canBeHit = true;
    bool m_isdead;

    public bool IsDead {  get { return m_isdead; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Danger" && m_canBeHit)
        {
            m_canBeHit = false;

            _onHit.Invoke();//Rajouté par Simon

            m_health--;
            if (m_health <= 0)
            {
                m_isdead = true;

                _onDeath.Invoke();//Rajouté par Simon

                m_canvasGameOver.SetActive(true);
            }
            else
            {
                StartCoroutine(WaitToBeHit());
            }
        }
    }

    IEnumerator WaitToBeHit()
    {
        for(int i = 0; i < m_invincibleSeconds / m_blinkTime; i++)
        {
            m_sR.enabled = !m_sR.enabled;
            yield return new WaitForSeconds(m_blinkTime);
        }

        m_sR.enabled = true;
        m_canBeHit = true;
        m_sR.color = new Color(1f, 1f, 1f, 1f);
    }
}
