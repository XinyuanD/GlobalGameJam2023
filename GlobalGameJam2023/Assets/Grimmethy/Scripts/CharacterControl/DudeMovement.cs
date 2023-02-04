using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeMovement : MonoBehaviour
{
    public int moveSpeed = 10;
    private float horizontalMovement;

    private Rigidbody2D rb;

    public int jumpForce = 20;
    private bool hasJumped = false;

    public bool onGround = false;

    public Collider2D floorCollider;
    public ContactFilter2D floorFilter;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        onGround = floorCollider.IsTouching(floorFilter);

        if(!hasJumped && onGround && Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (hasJumped)
        {
            hasJumped = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (rb.velocity.x > moveSpeed)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }

        if (rb.velocity.y > moveSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
    }
}
