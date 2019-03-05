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
    [SerializeField] private GameObject m_TimeSlowUI;

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
    private ParticleSystem m_BloodBurstParticles;
    private MoveState m_MoveState = MoveState.Run;
    private bool m_Attacking = false;
    private TimeSlow m_TimeSlow;

    // Use this for initialization
    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_FacingRight = true;
        m_GroundedPos = transform.Find("GroundedTransform");
        m_DustParticles = GameObject.Find("Dust").GetComponent<ParticleSystem>();
        m_BurstDustParticles = GameObject.Find("BurstDust").GetComponent<ParticleSystem>();
        m_BloodBurstParticles = GameObject.Find("BloodBurst").GetComponent<ParticleSystem>();
        m_DustEmission = m_DustParticles.emission;
        m_TimeSlow = m_TimeSlowUI.GetComponent<TimeSlow>();
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
            m_FrontLegAnim.SetBool("InAir", true);

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
            if(m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishDiveKick"))
            {
                if (m_FacingRight)
                {
                    m_RigidBody.velocity = new Vector2(30f, -30f);
                }
                else
                {
                    m_RigidBody.velocity = new Vector2(-30f, -30f);
                }
            }
            else if (h > 0 && m_RigidBody.velocity.x < m_MoveSpeed && !m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("Fire") && !m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishDiveKickJump"))
            {
                m_RigidBody.velocity = new Vector2(h * (m_RigidBody.velocity.x + m_RigidBody.velocity.normalized.x * 10 * Time.deltaTime) + h, m_RigidBody.velocity.y);
                if (m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishRun"))
                {
                    m_FrontLegAnim.speed = Mathf.Abs((m_RigidBody.velocity.x * 0.1f) + 0.5f);
                }
            }
            else if (h < 0 && m_RigidBody.velocity.x > -(m_MoveSpeed) && !m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("Fire") && !m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishDiveKickJump"))
            {
                m_RigidBody.velocity = new Vector2((-h * (m_RigidBody.velocity.x + m_RigidBody.velocity.normalized.x * 10 * Time.deltaTime) + h), m_RigidBody.velocity.y);
                if (m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("FishRun"))
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
                } else if (Input.GetButtonDown("Fire2"))
                {
                    m_FrontLegAnim.SetTrigger("Laser");
                    m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);
                }
                else if(Input.GetButtonDown("Fire4") && m_Grounded)
                {
                    m_FrontLegAnim.SetTrigger("DiveKick");
                }
            }

            if (m_FrontLegAnim.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
            {
                m_FrontLegAnim.speed = 1f;
                /*if (m_RigidBody.velocity.x >= 0)
                {
                    m_RigidBody.velocity = new Vector2(m_MoveSpeed * 2, 0f);
                } else if(m_RigidBody.velocity.x < 0)
                {
                    m_RigidBody.velocity = new Vector2(m_MoveSpeed * -2, 0f);
                }
                 
                */
                if (m_Attacking == false)
                {
                    StartCoroutine("Kick");
                }
                print("kick");
            }

            if (Input.GetButtonDown("Fire3"))
            {
                m_TimeSlow.ToggleTimeSlow();
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

    IEnumerator Kick()
    {
        m_Attacking = true;
        if (m_FacingRight)
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed * 2, 0f);
        }
        else
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed * -2, 0f);
        }
        yield return new WaitForSeconds(5f);
        /*if(m_FacingRight)
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed, 0f);
        }
        else
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed * -1, 0f);
        }*/
        m_RigidBody.velocity = new Vector2(0f, 0f);
        m_Attacking = false;
    }

    IEnumerator Divekick()
    {
        m_Attacking = true;
        m_RigidBody.velocity = new Vector2(0f, 25f);
        yield return new WaitForSeconds(0.5f);
        


        m_Attacking = false;
    }

    public void TakeDamage(int amount)
    {
        m_BloodBurstParticles.Play();
    }
}
