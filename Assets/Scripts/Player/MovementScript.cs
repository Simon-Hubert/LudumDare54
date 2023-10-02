using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] HitScript m_hitScript;
    [SerializeField] float m_moveSpeed;
    [SerializeField] InputActionReference m_inputMovement;

    [Header("Visual rotation")]
    [SerializeField] Transform m_visualTransform;
    [SerializeField] Transform m_center;

    private void FixedUpdate()
    {
        Vector2 moveInput = m_inputMovement.action.ReadValue<Vector2>();
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;

        if (!m_hitScript.IsDead)
        {
            //m_rb.MovePosition(currentPosition + moveInput * m_moveSpeed * Time.deltaTime);
            m_rb.velocity = (currentPosition - transform.position) * m_moveSpeed;

            //Regarde tour
            Vector2 direction = m_center.position - transform.position;
            float dist = direction.magnitude;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            m_visualTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        }
    }
}