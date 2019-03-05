using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    public AnimationCurve m_LerpCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    private Camera m_Camera;
    private bool m_AnimPlayed;
    private bool m_OtherAnimPlayed;
    private float m_StepNum;
    private float m_BackStepNum;
    private float m_StartSize;

    private float m_CurrentLerpTime;
    // Use this for initialization
    void Start ()
    {
        m_Camera = GetComponent<Camera>();
        m_StartSize = m_Camera.orthographicSize;
        m_StepNum = 0.0f;
        m_BackStepNum = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(m_AnimPlayed == false)
        {
            m_StepNum += Time.deltaTime;
            float perc = m_StepNum / 1f;
            PositionSizeLerp(0, -2.5f, m_StartSize, 4, perc);
            if (m_Camera.orthographicSize == 4)
            {
                m_AnimPlayed = true;
            }
        }

        if(m_AnimPlayed && GlobalValues.GAME_STATE == GlobalValues.GameState.Playing && m_OtherAnimPlayed == false) 
        {
            m_BackStepNum += Time.deltaTime;
            float perc = m_BackStepNum / 1f;
            PositionSizeLerp(-2.5f, 0, 4, m_StartSize, perc);
            if (m_Camera.orthographicSize == m_StartSize)
            {
                m_OtherAnimPlayed = true;
            }
        }
	}

    void PositionSizeLerp(float startPos, float endPos, float startSize, float endSize, float percentage)
    {
        m_Camera.orthographicSize = Mathf.Lerp(startSize, endSize, m_LerpCurve.Evaluate(percentage));
        gameObject.transform.localPosition = new Vector3(Mathf.Lerp(startPos, endPos, m_LerpCurve.Evaluate(percentage)), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }
}
