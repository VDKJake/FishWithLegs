using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoot : MonoBehaviour {

    private PlayerMovement m_PlayerScript;

	// Use this for initialization
	void Start ()
    {
        m_PlayerScript = GameObject.Find("Fish").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_PlayerScript.TakeDamage(1);
        }
    }
}
