using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 10f;
    private float moveInput;

    private bool isFacingRight = true;

    private bool isGrounded;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float checkRadius = 0.25f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float jumpTime = 0.3f;
    private float jumpTimeCounter;
    private bool isJumping;
    [SerializeField] private float jumpPressedRememberTime = 0.2f;
    private float jumpPressedRememberCounter;
    [SerializeField] private float groundedRememberTime = 0.2f;
    private float groundedRememberCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isFacingRight && moveInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput > 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        groundedRememberCounter -= Time.deltaTime;

        if (isGrounded)
        {
            groundedRememberCounter = groundedRememberTime;
        }

        jumpPressedRememberCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressedRememberCounter = jumpPressedRememberTime;
        }

        if (jumpPressedRememberCounter > 0 && groundedRememberCounter > 0)
        {
            groundedRememberCounter = 0;
            jumpPressedRememberCounter = 0;

            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
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
