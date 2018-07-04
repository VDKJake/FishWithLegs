using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisions : MonoBehaviour {

    Rigidbody2D m_rb;
    float test;
    float testy;
    HingeJoint2D m_HingeJoint;
    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_HingeJoint = GetComponent<HingeJoint2D>();
        m_HingeJoint.useLimits = true;
        //JointAngleLimits2D temp = new JointAngleLimits2D();
        //temp.max = 6f;
        //temp.min = -6f;
        //m_HingeJoint.limits = temp;
        //m_rb.centerOfMass = new Vector2(0, 0);
        //m_rb.inertia = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // m_rb.velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x, GetComponentInParent<Rigidbody2D>().velocity.y);
        //transform.localPosition = new Vector3(-1.78f, -0.15f, 0);
        if(transform.localPosition.x < test)
        {
            test = transform.localPosition.x;
            //print("X" + test);
        }

        if (transform.localPosition.y < testy)
        {
            test = transform.localPosition.y;
            //print("Y" + test);
        }


        /*if (transform.localPosition.x != -1.773674)
        {
            transform.localPosition = new Vector3(-1.773674f, transform.localPosition.y);
        }
        if (transform.localPosition.y < -0.2404627)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0.2404627f);
        } else if(transform.localPosition.y > -0.05953819)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.05953819f);
        }*/
        
        /*print(transform.localRotation.z);

        if(transform.localRotation.z < -6)
        {
            transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, -6f, transform.localRotation.w);
            print("too small");
        } else if(transform.localRotation.z > 6)
        {
            print("too big");
            transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, 6f, transform.localRotation.w);
        }*/
        
        //print(m_HingeJoint.jointAngle);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
    }
}
