using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private float m_direction = 0;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move (float direction)
    {
        m_direction = direction;
    }

    public void Stop ()
    {
        m_direction = 0;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = Vector2.right * m_direction * speed;
    }
}
