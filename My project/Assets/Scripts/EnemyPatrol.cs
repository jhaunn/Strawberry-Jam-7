using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    private bool isFacingRight = true;
    [SerializeField] private bool isMovingRight = true;

    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float checkRadius = 0.25f;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    [SerializeField] private float waitTime;
    private float currentWaitTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentWaitTime = waitTime;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            if (isMovingRight)
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            }
            else if (!isMovingRight)
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            }
        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            currentWaitTime -= Time.deltaTime;

            if (currentWaitTime <= 0f)
            {
                currentWaitTime = waitTime;

                isMovingRight = !isMovingRight;
            }
        }


        if (isMovingRight && !isFacingRight)
        {
            Flip();
        }

        if (!isMovingRight && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
