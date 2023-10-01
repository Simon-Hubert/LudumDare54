using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] AnimationCurve m_curve;

    [Header("Test Only")]
    [SerializeField, Range(0f, 5f)] float m_duree;
    [SerializeField, Range(0f, 5f)] float m_force;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    IEnumerator Shake(float duration, float strenght)
    {
        Vector3 startPos = transform.position;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float curve = m_curve.Evaluate(currentTime / duration);
            transform.position = startPos + Random.insideUnitSphere * curve * strenght;
            yield return null;

            Debug.Log(transform.position + " | " + currentTime);
        }

        transform.position = startPos;
    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Test Shake"))
        {
            StartCoroutine(Shake(m_duree, m_force));
        }
    }*/
}
