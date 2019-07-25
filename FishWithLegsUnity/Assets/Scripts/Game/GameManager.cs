using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_MapPrefab1;
    [SerializeField] private GameObject m_MapPrefab2;
    [SerializeField] private GameObject m_MapPrefab3;
    [SerializeField] private GameObject m_MapPrefab4;
    [SerializeField] private GameObject m_EnemyPrefab;

    [SerializeField] private GameObject m_PlayerSprite;
    [SerializeField] private GameObject m_FinSprite;
    [SerializeField] private GameObject m_TailSprite;
    [SerializeField] private GameObject m_HeadTopSprite;
    [SerializeField] private GameObject m_HeadBottomSprite;

    public Slider[] m_FishSliders;
    private GameObject m_UI;
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

        LoadLevel();
        //Time.timeScale = 0f;
        
        m_FishSliders[0].value = Random.Range(0f, 255f);
        m_FishSliders[1].value = Random.Range(0f, 255f);
        m_FishSliders[2].value = Random.Range(0f, 255f);
    }
	
	void Update ()
    {
        if(GlobalValues.GAME_STATE == GlobalValues.GameState.Startup)
        {
            print(m_FishSliders[0].value);
            m_PlayerSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
            m_FinSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
            print(m_FishSliders[0].value / 255);
            print(m_FinSprite.GetComponent<SpriteRenderer>().color.r);
            m_HeadTopSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
            m_HeadBottomSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
            m_TailSprite.GetComponent<SpriteRenderer>().color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
            m_ColourPreview.color = new Color(m_FishSliders[0].value / 255, m_FishSliders[1].value / 255, m_FishSliders[2].value / 255);
        }
    }

    public void StartGame()
    {
        GlobalValues.GAME_STATE = GlobalValues.GameState.Playing;
        m_StartupUI.SetActive(false);
    }

    private void LoadLevel()
    {
        /*bool includeWater = false;
        includeWater = true;
        float xPos = 0f;
        int num;

        GameObject instantiatedObject;
        GameObject enemy;

        if(includeWater == true)
        {
            num = Random.Range(1, 5);
        } else
        {
            num = Random.Range(1, 3);
        }
        
        for (int i = 0; i < 20; i++)
        {
            switch(num)
            {
                case 1:
                    instantiatedObject = Instantiate(m_MapPrefab1, new Vector3(xPos, -50, 0), new Quaternion());
                    enemy = Instantiate(m_EnemyPrefab, new Vector3(xPos, 0, 0), new Quaternion());
                    enemy.transform.parent = instantiatedObject.transform;
                    enemy.transform.localPosition = new Vector3(-7.7f, 1.15f, 0);
                    break;
                case 2:
                    instantiatedObject = Instantiate(m_MapPrefab2, new Vector3(xPos, -50, 0), new Quaternion());
                    enemy = Instantiate(m_EnemyPrefab, new Vector3(xPos, 0, 0), new Quaternion());
                    enemy.transform.parent = instantiatedObject.transform;
                    enemy.transform.localPosition = new Vector3(-7.7f, 1.15f, 0);
                    break;
                case 3:
                    instantiatedObject = Instantiate(m_MapPrefab3, new Vector3(xPos, -50, 0), new Quaternion());
                    break;
                case 4:
                    instantiatedObject = Instantiate(m_MapPrefab4, new Vector3(xPos, -50, 0), new Quaternion());
                    break;
            }

            if (num == 3)
            {
                print("num = 3");
                num = 4;
                ; } else if (num == 4)
            {
                print("num = 4");
                num = 2;
            } else if (num == 2 && includeWater == false)
            {
                num = Random.Range(1, 4);
                print(num);
            } else if (num == 1 || includeWater == true)
            {
                
                num = Random.Range(1, 3);
                print(num);
            }

            xPos += 20;
        }*/
    }
}
