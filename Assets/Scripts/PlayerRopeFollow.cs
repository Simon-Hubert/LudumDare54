using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeFollow : MonoBehaviour
{
    [SerializeField] Transform m_player;
    void Update()
    {
        transform.position = m_player.position;
    }
}
