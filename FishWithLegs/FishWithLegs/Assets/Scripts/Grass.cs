using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private ParticleSystem m_GrassPart;

	// Use this for initialization
	void Start ()
    {
       m_GrassPart = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!m_GrassPart.isPlaying)
            {
                m_GrassPart.Play();
            }
        }
    }
}
