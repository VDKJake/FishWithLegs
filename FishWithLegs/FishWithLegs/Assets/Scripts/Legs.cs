using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    [SerializeField] private AudioSource m_ParentAudio;
    [SerializeField] private AudioClip m_StepClip;

    private PlayerMovement m_MoveScript;

	// Use this for initialization
	void Start ()
    {
        m_MoveScript = GetComponentInParent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayStep()
    {
        m_ParentAudio.PlayOneShot(m_StepClip);
    }

    public void DustBurst()
    {
        m_MoveScript.DustBurst();
    }
}
