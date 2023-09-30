using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ChainManager : MonoBehaviour
{
    private List<Transform> m_chainSegments = new List<Transform>();
    [SerializeField] GameObject m_ChainPrefab;
    [SerializeField] Transform m_centerPoint;
    [SerializeField] Transform m_playerTransform;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float m_SegmentLen;
    [SerializeField] private int m_numberOfSegments;

    public int numberOfSegments { get => m_numberOfSegments; }

    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Awake()
    {
        ChangeLength(m_numberOfSegments);
    }

    private void Update()
    {
        //Contraintes
        for (int i = 0; i < m_numberOfSegments + 1; i++)
        {
            Transform firstSegment = m_chainSegments[0];
            firstSegment.position = m_centerPoint.position;
            m_chainSegments[0] = firstSegment;

            for (int j = 0; j < m_numberOfSegments; j++)
            {
                Transform m_currentSegment = m_chainSegments[j];
                Transform m_nextSegment = m_chainSegments[j + 1];

                float dist = (m_currentSegment.position - m_nextSegment.position).magnitude;
                float error = Mathf.Abs(dist - m_SegmentLen);

                Vector2 changeDir = Vector2.zero;
                if (dist > m_SegmentLen) changeDir = (m_currentSegment.position - m_nextSegment.position).normalized;
                else if (dist < m_SegmentLen) changeDir = (m_nextSegment.position - m_currentSegment.position).normalized;

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

    private void LateUpdate()
    {
        //Display Chain

        for (int i = 0; i < m_numberOfSegments + 1; i++)
        {
            lineRenderer.SetPosition(i, m_chainSegments[i].position);
        }
    }

    [Header("Debug")]
    [SerializeField] int DEBUG_LENGTH;
    [Button] void DEBUG_ChangeLength() => ChangeLength(DEBUG_LENGTH);

    public void ChangeLength(int newLength)
    {
        if(m_chainSegments.Count != 0) // Clear list
        {
            foreach (Transform transform in m_chainSegments)
            {
                if(transform.gameObject.tag != "Player") Destroy(transform.gameObject);
            }
            m_chainSegments.Clear();
        }

        // Création de la chaine

        Vector3 chainStartPoint = m_centerPoint.position;


        Rigidbody2D old_rb = null;
        for (int i = 0; i < newLength; i++)
        {
            GameObject newChainObject = Instantiate(m_ChainPrefab, chainStartPoint, Quaternion.identity);

            if (old_rb != null)
            {
                newChainObject.GetComponent<HingeJoint2D>().connectedBody = old_rb;
            }
            old_rb = newChainObject.GetComponent<Rigidbody2D>();

            m_chainSegments.Add(newChainObject.transform);

            //Détermination de la direction de création des points de chaines
            //T'inquiète...
            Vector3 chainPointDir = m_playerTransform.position - m_centerPoint.position;
            chainPointDir.Normalize();
            chainStartPoint += m_SegmentLen * chainPointDir;
        }

        m_chainSegments.Add(m_playerTransform);
        m_playerTransform.GetComponent<HingeJoint2D>().connectedBody = old_rb;
        m_playerTransform.position = chainStartPoint;

        lineRenderer.positionCount = newLength + 1;
        m_numberOfSegments = newLength;
    }
}
