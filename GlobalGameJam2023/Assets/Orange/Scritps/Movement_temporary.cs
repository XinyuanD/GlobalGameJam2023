using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_temporary : MonoBehaviour
{
    [Header("Player Self Movements")]
    public Collider2D collider2d;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public bool hasJumped = false;
    public float jumpSpeed = 10f;
    public float jumpHeightModifier = 0.5f;
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    //public float jumpBufferTime = 0.2f;
    //public float jumpBufferCounter;
    public float spriteScale;

    [Header("Movements Due to Environment")]
    public bool onGround = false;
    public bool mushroomJump = false;
    public float mushroonJumpSpeed = 15f;
    public float enemyBounceSpeed = 15f;

    void Start()
    {
        collider2d = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteScale = transform.localScale.x;
    }

    void FixedUpdate()
    {
        Move();
        JumpLogic();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (coyoteTimeCounter > 0f && context.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightModifier);
            coyoteTimeCounter = 0f;
        }
        
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector2(-spriteScale, spriteScale);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector2(spriteScale, spriteScale);
        }
    }

    private void JumpLogic()
    {
        onGround = collider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (onGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //check if is jumpshroom
        if (other.gameObject.tag == "JumpShroom")
        {
            if (!onGround)
            {
                mushroomJump = true;
                rb.velocity = new Vector2(rb.velocity.x, mushroonJumpSpeed);
            }
        }

        // Check if is enemy
        if (other.gameObject.tag == "Enemy")
        {
            rb.velocity = new Vector2(rb.velocity.x, enemyBounceSpeed);
        }
    }
}
