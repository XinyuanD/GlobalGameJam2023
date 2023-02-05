using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Player Self Movements")]
    public Collider2D bodyCollider;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask vineLayer;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public bool hasJumped = false;
    public float jumpSpeed = 10f;
    public float jumpHeightModifier = 0.5f;
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    public float spriteScale;

    public int wallJumpCount = 1;
    private int wallJumps;

    [Header("Movements Due to Environment")]
    public bool onGround = false;
    public bool mushroomJump = false;
    public float mushroonJumpSpeed = 15f;
    public float enemyBounceSpeed = 15f;

    private Vector3 lastPos;

    private Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteScale = transform.localScale.x;

        lastPos = transform.position;
    }

    void Update()
    {
        SetRunAnim();
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
        // coyote timer is for allowing the player to jump if they have just left the ground without jumping
        // I'm not a fan of this jump mechanic, it feels clumsy. I'd like to find a way to reintegrate different jump heights using addforce for a smoother experience
        if (coyoteTimeCounter > 0f && context.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpHeightModifier);
            coyoteTimeCounter = 0f;
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (IsOnVine() && anim.GetBool("Jumping") == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveInput.y * moveSpeed);
            coyoteTimeCounter = coyoteTime;
        }

        if (moveInput.x > 0)
        {
            transform.localScale = new Vector2(spriteScale, spriteScale);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector2(-spriteScale, spriteScale);
        }
    }

    private void JumpLogic()
    {
        onGround = IsGrounded();
        if (onGround)
        {
            wallJumps = 0;
            coyoteTimeCounter = coyoteTime;
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, 0.2f, groundLayer);
    }

    private bool IsOnVine()
    {
        return Physics2D.OverlapCircle(feet.position, 0.2f, vineLayer);
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

        if (other.gameObject.tag == "Ground" && wallJumps < wallJumpCount)
        {
            onGround = true;
            coyoteTimeCounter = coyoteTime;
            wallJumps++;
        }

        // Check if is enemy
        if (other.gameObject.tag == "Enemy")
        {
            if (!onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, enemyBounceSpeed);
            }
            else
            {
                gameManager.InvokePlayerDeath();
            }
        }

        if (other.gameObject.tag == "Spike")
        {
            gameManager.InvokePlayerDeath();
        }
    }

    void SetRunAnim()
    {
        Vector3 posChange = transform.position - lastPos;
        if (Mathf.Abs(posChange.x) >= .01f || Mathf.Abs(posChange.z) >= .01f)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        lastPos = transform.position;
    }
}
