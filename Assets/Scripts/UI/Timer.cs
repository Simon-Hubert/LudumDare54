using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_text;

    [SerializeField] Slider _progressBar;
    [SerializeField] float _totalGameTime = 257;
    float _timer = 0;

    [SerializeField] UnityEvent _onPhaseChange; //Rajouté par Simon
    [SerializeField] UnityEvent _onWin; //Rajouté par Simon

    float m_currentTime;
    int m_phase;


    // delegate void OnPhaseChangingDelegate(int phase);
    // static OnPhaseChangingDelegate OnPhaseChanging;

    public int Phase { get => m_phase;} //Rajouté par Simon

    private void Start()
    {
        m_phase = 0;
        m_currentTime = 0f;

        PhaseChange();
    }

    private void FixedUpdate()
    {
        m_currentTime -= Time.fixedDeltaTime;
        //m_text.text = Mathf.RoundToInt(m_currentTime).ToString();

        _timer += Time.fixedDeltaTime;
        _progressBar.value = _timer/_totalGameTime;

        if (m_currentTime <= 0f) PhaseChange();
    }

    void PhaseChange()
    {
        m_phase++;
        //OnPhaseChanging.Invoke(m_phase);
        
        switch (m_phase)
        {
            case 1 :
                m_currentTime += 48f;
                break;

            case 2:
                m_currentTime += 93f;
                _onPhaseChange.Invoke(); //Rajouté Par Simon
                break;

            case 3:
                m_currentTime += 105f;
                _onPhaseChange.Invoke(); //Rajouté Par Simon
                break;
            case 4://Rajouté par Simon
                _onWin.Invoke();
                break;

            default :
                m_currentTime = 0;
                break;
        }
    }
}
