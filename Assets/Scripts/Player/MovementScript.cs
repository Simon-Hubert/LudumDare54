using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] float m_moveSpeed;
    [SerializeField] InputActionReference m_inputMovement;

    [Header("Visual rotation")]
    [SerializeField] Transform m_visualTransform;
    [SerializeField] Transform m_center;

    private void FixedUpdate()
    {
        Vector2 moveInput = m_inputMovement.action.ReadValue<Vector2>();
        Vector2 currentPosition = transform.position;

        if (moveInput != Vector2.zero)
        {
            m_rb.MovePosition(currentPosition + moveInput * m_moveSpeed * Time.deltaTime);


            Vector2 direction = m_center.position - transform.position;//Get the direction and distance.
            float dist = direction.magnitude;//Get the distance first
            direction.Normalize();//Turn it into an actual direction vector.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Atan2 gives the angle in radians, Unity works in degrees.

            m_visualTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        }
    }
}