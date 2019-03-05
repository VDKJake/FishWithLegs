using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameObject m_Player;

	// Use this for initialization
	void Start ()
    {
        m_Player = GameObject.Find("Fish");
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_Player.transform.position - transform.position, 50f);

        if(hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            if (hit.distance < 40f)
            {
                if (m_Player.transform.position.x < transform.position.x)
                {
                    transform.Translate(-0.1f, 0f, 0f);
                }
                else
                {
                    transform.Translate(0.1f, 0f, 0f);
                }
            }
        }
	}
}
