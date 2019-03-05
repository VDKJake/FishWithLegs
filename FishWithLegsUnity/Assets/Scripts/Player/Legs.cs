using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    [SerializeField] private AudioSource m_ParentAudio;
    [SerializeField] private AudioClip m_StepClip;
    [SerializeField] private BoxCollider2D m_KickCollider;
    [SerializeField] private AudioClip m_LaserChargeClip;

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

    public void Kick()
    {
        m_KickCollider.enabled = true;
    }

    public void EndKick()
    {
        m_KickCollider.enabled = false;
    }

    public void LaserCharge()
    {
        m_ParentAudio.PlayOneShot(m_LaserChargeClip);
    }

    public void Divekick()
    {
        m_MoveScript.StartCoroutine("Divekick");
    }
}
