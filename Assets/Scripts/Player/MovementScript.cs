using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;
    [SerializeField] InputActionReference m_inputMovement;

    private void FixedUpdate()
    {
        Vector2 moveInput = m_inputMovement.action.ReadValue<Vector2>();
        transform.Translate(moveInput * m_moveSpeed * Time.deltaTime);
    }
}
