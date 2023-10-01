using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_text;

    float m_currentTime;
    int m_phase;


    delegate void OnPhaseChangingDelegate(int phase);
    static OnPhaseChangingDelegate OnPhaseChanging;

    private void Start()
    {
        m_phase = 0;
        m_currentTime = 0f;

        PhaseChange();
    }

    private void FixedUpdate()
    {
        m_currentTime -= Time.fixedDeltaTime;
        m_text.text = Mathf.RoundToInt(m_currentTime).ToString();

        if (m_currentTime <= 0f) PhaseChange();
    }

    void PhaseChange()
    {
        m_phase++;
        //OnPhaseChanging.Invoke(m_phase);
        
        switch (m_phase)
        {
            case 1 :
                m_currentTime += 120f;
                break;

            case 2:
                m_currentTime += 90f;
                break;

            case 3:
                m_currentTime += 60f;
                break;

            default :
                m_currentTime = 0;
                break;
        }
    }
}
