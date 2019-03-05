using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlow : MonoBehaviour {

    private bool m_Active;
    private float m_Timer;
    private Slider m_TimeSlowSlider;

    [SerializeField] private float m_TimeSlowDuration;
	// Use this for initialization
	void Start ()
    {
        m_Active = false;
        m_Timer = m_TimeSlowDuration;
        m_TimeSlowSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(m_Active)
        {
            m_Timer -= Time.deltaTime;
            Time.timeScale = 0.5f;
        }else if(!m_Active)
        {
            if(m_Timer > m_TimeSlowDuration)
            {
                m_Timer = m_TimeSlowDuration;
            }
            else
            {
                m_Timer += Time.deltaTime;
            }
            Time.timeScale = 1f;
        }

        if(m_Timer <= 0)
        {
            m_Timer = 0;
            m_Active = false;
        }

        float percent = m_Timer / m_TimeSlowDuration;
        m_TimeSlowSlider.value = percent;
	}

    public void ToggleTimeSlow()
    {
        m_Active = !m_Active;
    }
}
