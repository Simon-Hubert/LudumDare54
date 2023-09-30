using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    private List<Transform> m_chainSegments = new List<Transform>();
    [SerializeField] GameObject m_ChainPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float m_chainSegLen;
    [SerializeField] private int m_segmentLength;

    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Awake()
    {
        // Création de la chaine

        Vector3 chainStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Rigidbody2D old_rb = null;
        for (int i = 0; i < m_segmentLength; i++)
        {
            GameObject newChainObject = Instantiate(m_ChainPrefab, chainStartPoint, Quaternion.identity);
            
            if(old_rb != null) 
            {
                newChainObject.GetComponent<HingeJoint2D>().connectedBody = old_rb;
            }         
            old_rb = newChainObject.GetComponent<Rigidbody2D>();

            m_chainSegments.Add(newChainObject.transform);
            chainStartPoint.y -= m_chainSegLen;
        }
    }

    private void Update()
    {
        //Display Chain

        for (int i = 0;i < m_segmentLength;i++)
        {
            lineRenderer.SetPosition(i, m_chainSegments[i].position);
        }

    }

    private void FixedUpdate()
    {
        //Contraintes

        m_chainSegments[0].position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < m_segmentLength; i++)
        {
            Transform firstSegment = m_chainSegments[0];
            firstSegment.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_chainSegments[0] = firstSegment;

            for (int j = 0; j < m_segmentLength - 1; j++)
            {
                Transform m_currentSegment = m_chainSegments[j];
                Transform m_nextSegment = m_chainSegments[j + 1];
                
                float dist = (m_currentSegment.position - m_nextSegment.position).magnitude;
                float error = Mathf.Abs(dist - m_chainSegLen);

                Vector2 changeDir = Vector2.zero;
                if (dist > m_chainSegLen) changeDir = (m_currentSegment.position - m_nextSegment.position).normalized;
                else if (dist < m_chainSegLen) changeDir = (m_nextSegment.position - m_currentSegment.position).normalized;

                Vector2 changeAmount = changeDir * error;
                if (j != 0)
                {
                    Vector2 m_currentSegPos = m_currentSegment.position;
                    m_currentSegPos -= changeAmount * 0.5f;
                    m_currentSegment.position = m_currentSegPos;
                    m_chainSegments[j] = m_currentSegment;

                    Vector2 m_nextSegPos = m_nextSegment.position;
                    m_nextSegPos += changeAmount * 0.5f;
                    m_nextSegment.position = m_nextSegPos;
                    m_chainSegments[j + 1] = m_nextSegment;
                }
                else
                {
                    Vector2 m_nextSegPos = m_nextSegment.position;
                    m_nextSegPos += changeAmount;
                    m_nextSegment.position = m_nextSegPos;

                    m_chainSegments[j + 1] = m_nextSegment;
                }
                
            }
        }
    }
}
