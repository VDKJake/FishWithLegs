using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider[] m_FishSliders;
    private GameObject m_UI;
    private GameObject m_PlayerSprite;
    private GameObject m_StartupUI;
    private Image m_ColourPreview;

    private Vector3 m_CameraStartPos = new Vector3(0, -0.32f, -10);

	void Start ()
    {
        GlobalValues.GAME_STATE = GlobalValues.GameState.Startup;
        m_UI = GameObject.Find("UI");
        m_StartupUI = GameObject.Find("Startup");
        m_ColourPreview = GameObject.Find("ColourPreview").GetComponent<Image>();
        m_FishSliders = m_UI.GetComponentsInChildren<Slider>();;

        m_PlayerSprite = GameObject.Find("FishSprite");

        //Time.timeScale = 0f;
	}
	
	void Update ()
    {
        m_PlayerSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
        m_ColourPreview.color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
    }

    public void StartGame()
    {
        GlobalValues.GAME_STATE = GlobalValues.GameState.Playing;
        m_StartupUI.SetActive(false);
    }
}
