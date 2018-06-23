using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour {

    private Camera m_Camera;
    private bool m_AnimPlayed;
    private bool m_OtherAnimPlayed;
    private float m_StepNum;
    private float m_BackStepNum;
    // Use this for initialization
    void Start ()
    {
        m_Camera = GetComponent<Camera>();
        m_StepNum = 0.0f;
        m_BackStepNum = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_StepNum += 2f * Time.deltaTime;
		if(m_AnimPlayed == false)
        {
            m_Camera.orthographicSize = Mathf.Lerp(10, 4, m_StepNum);
            gameObject.transform.localPosition = new Vector3(Mathf.Lerp(0, -2.5f, m_StepNum), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            if(m_Camera.orthographicSize == 4)
            {
                m_AnimPlayed = true;
            }
        }

        if(m_AnimPlayed && GlobalValues.GAME_STATE == GlobalValues.GameState.Playing && m_OtherAnimPlayed == false) 
        {
            m_BackStepNum += 2f * Time.deltaTime;
            gameObject.transform.localPosition = new Vector3(Mathf.Lerp(-2.5f, 0, m_BackStepNum), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            m_Camera.orthographicSize = Mathf.Lerp(4, 10, m_BackStepNum);
            if (m_Camera.orthographicSize == 10)
            {
                m_OtherAnimPlayed = true;
                print("anmim done");
            }
        }
	}
}
