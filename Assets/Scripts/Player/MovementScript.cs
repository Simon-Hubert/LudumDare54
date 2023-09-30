using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_rb;
    [SerializeField] float m_moveSpeed;
    [SerializeField] InputActionReference m_inputMovement;

    private void FixedUpdate()
    {
        Vector2 moveInput = m_inputMovement.action.ReadValue<Vector2>();
        Vector2 currentPosition = transform.position;

        if (moveInput != Vector2.zero)
        {
            m_rb.MovePosition(currentPosition + moveInput * m_moveSpeed * Time.deltaTime);
        }
        
    }
}
