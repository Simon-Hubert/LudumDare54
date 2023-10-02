using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTOTEM : MonoBehaviour
{
    [SerializeField] Transform m_playerTransform;
    [SerializeField] float m_speed;
    private void FixedUpdate()
    {
        //Regarde tour
        Vector2 direction = m_playerTransform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion playerRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * m_speed);
    }
}
