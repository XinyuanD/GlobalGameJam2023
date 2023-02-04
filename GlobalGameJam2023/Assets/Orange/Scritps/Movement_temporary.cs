using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_temporary : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpSpeed = 10f;
    public float jumpHeightModifier = 0.5f;
    public float spriteScale;

    private bool hasJumped = false;
    public bool onGround = false;
    public bool mushroomJump = false;

    public Collider2D collider2d;
    private Rigidbody2D rb;

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
        if (context.performed && !hasJumped && onGround)
        {
            hasJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if (context.canceled && rb.velocity.y > Mathf.Epsilon)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightModifier);
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

        if (hasJumped)
        {
            hasJumped = false;
        }
    }
}
