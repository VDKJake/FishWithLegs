using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Animator m_FrontLegAnim;
    //[SerializeField] private Animator m_BackLegAnim;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private float m_JumpHeight;
    [SerializeField] private List<Rigidbody2D> m_LegRigidBody = new List<Rigidbody2D>();

    enum MoveState { Run, Spin };

    private Rigidbody2D m_RigidBody;
    private bool m_FacingRight;
    private bool m_JumpPressed;
    private bool m_Grounded;
    private float m_GroundRadius = 2f;
    private Transform m_GroundedPos;
    private ParticleSystem m_DustParticles;
    private ParticleSystem.EmissionModule m_DustEmission;
    private ParticleSystem m_BurstDustParticles;
    private MoveState m_MoveState = MoveState.Run;

    // Use this for initialization
    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
        m_GroundedPos = transform.Find("GroundedTransform");
        m_DustParticles = GameObject.Find("Dust").GetComponent<ParticleSystem>();
        m_BurstDustParticles = GameObject.Find("BurstDust").GetComponent<ParticleSystem>();
        m_DustEmission = m_DustParticles.emission;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!m_JumpPressed)
        {
            m_JumpPressed = Input.GetButtonDown("Jump");
        }
		
	}

    private void FixedUpdate()
    {
        if (GlobalValues.GAME_STATE == GlobalValues.GameState.Playing)
        {
            // Grounded Check (from standard assets)
            m_Grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundedPos.position, m_GroundRadius, m_GroundLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    m_FrontLegAnim.SetBool("InAir", false);
                }
            }

            // Move & Set animations
            // What the fuck is this code
            float h = Input.GetAxis("Horizontal");
            if (h > 0 && m_RigidBody.velocity.x < m_MoveSpeed)
            {
                m_RigidBody.velocity = new Vector2(h * (m_RigidBody.velocity.x + m_RigidBody.velocity.normalized.x * 10 * Time.deltaTime) + h, m_RigidBody.velocity.y);
                if (m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishRun"))
                {
                    m_FrontLegAnim.speed = Mathf.Abs((m_RigidBody.velocity.x * 0.1f) + 0.5f);
                }
            }
            else if (h < 0 && m_RigidBody.velocity.x > -(m_MoveSpeed))
            {
                m_RigidBody.velocity = new Vector2((-h * (m_RigidBody.velocity.x + m_RigidBody.velocity.normalized.x * 10 * Time.deltaTime) + h), m_RigidBody.velocity.y);
                if(m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishRun"))
                {
                    m_FrontLegAnim.speed = Mathf.Abs((m_RigidBody.velocity.x * 0.1f) - 0.5f);
                }
            }
            else if (m_RigidBody.velocity.x == 0f)
            {
                m_FrontLegAnim.speed = 1f;
            }
            //m_RigidBody.velocity = new Vector2(h * (m_RigidBody.velocity.x + m_RigidBody.velocity.normalized.x * 10 * Time.deltaTime) + h, m_RigidBody.velocity.y);
            m_FrontLegAnim.SetFloat("Velocity", m_RigidBody.velocity.x);


            /*if(m_RigidBody.velocity.x != 0 && m_Grounded)
            {
                m_DustParticles.Play();
                m_DustEmission.rateOverTime = 10 * (1 + Mathf.Abs(m_RigidBody.velocity.x));
            }
            else
            {
                m_DustParticles.Stop();
            }*/

            // Flip Sprite
            if (m_RigidBody.velocity.x < -0.1 && m_FacingRight == true)
            {
                Flip();
            }
            else if (m_RigidBody.velocity.x > 0.1 && m_FacingRight == false)
            {
                Flip();
            }

            // Jump
            if (m_Grounded && m_JumpPressed)
            {
                m_Grounded = false;
                m_RigidBody.AddForce(new Vector2(0f, m_JumpHeight));
                m_FrontLegAnim.SetBool("InAir", true);
            }


            if (m_Grounded)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    m_FrontLegAnim.speed = 1f;
                    m_FrontLegAnim.SetTrigger("Kick");
                }
            }

            if (m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
            {
                m_FrontLegAnim.speed = 1f;
                m_RigidBody.velocity = new Vector2(20f, 0f);
                print("kick");
            }

            m_JumpPressed = false;
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void DustBurst()
    {
        if(m_Grounded == true)
        {
            m_BurstDustParticles.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pickup" && m_MoveState != MoveState.Spin)
        {
            m_MoveState = MoveState.Spin;
            m_MoveSpeed *= 1.5f;
            m_FrontLegAnim.SetTrigger("Spin");
        }
    }
}
