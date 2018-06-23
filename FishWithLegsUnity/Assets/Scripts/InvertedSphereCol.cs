using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedSphereCol : MonoBehaviour {

    [SerializeField] int m_NumEdges;
    [SerializeField] float m_Radius;

	void Start ()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] edgePoints = new Vector2[m_NumEdges];

        for(int i = 0; i < m_NumEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / m_NumEdges;
            float x = -(m_Radius * Mathf.Cos(angle));
            float y = -(m_Radius * Mathf.Sin(angle));

            edgePoints[i] = new Vector2(x, y);
        }
        edgeCollider.points = edgePoints;
	}

}
