using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LookPlayerShoot : MonoBehaviour
{
    [SerializeField] Transform m_playerTransform;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_waitInterval;
    bool m_canShoot = true;

    private void OnEnable()
    {
        m_canShoot = false;
        StartCoroutine(WaitForShoot(m_waitInterval));
    }

    private void FixedUpdate()
    {
        transform.LookAt(m_playerTransform);

        if (m_canShoot)
        {
            GameObject bullet = Instantiate(m_bulletPrefab, transform);
            StartCoroutine(WaitForShoot(m_waitInterval));
        }
    }

    IEnumerator WaitForShoot(float seconds)
    {
        yield return new WaitForSeconds(m_waitInterval);
        m_canShoot = true;
    }
}
