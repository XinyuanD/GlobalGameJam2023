using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DudeMovement : MonoBehaviour
{
    public int moveSpeed = 10;
    private float horizontalMovement;

    private Rigidbody2D rb;

    public int jumpForce = 10;
    private bool hasJumped = false;

    public bool onGround = false;
    public bool mushroomJump = false;

    public Collider2D floorCollider;
    public ContactFilter2D floorFilter;

    private SpriteRenderer charBody;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        charBody = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != Mathf.Epsilon)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
        }

        //onGround = floorCollider.IsTouching(floorFilter);
        onGround = floorCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (onGround == true)
        {
            mushroomJump = false;
        }

        if(!hasJumped && onGround && Input.GetKeyDown(KeyCode.Space))
        {
            hasJumped = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (horizontalMovement < 0)
        {
            charBody.flipX = false;
        }
        else if(horizontalMovement > 0)
        {
            charBody.flipX = true;
        }

        if (hasJumped)
        {
            hasJumped = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (mushroomJump == false)
        {
            if (rb.velocity.x > moveSpeed)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }

            if (rb.velocity.y > moveSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            }
        }
        else
        {
            if (rb.velocity.x > moveSpeed * 1.5f)
            {
                rb.velocity = new Vector2(moveSpeed * 1.5f, rb.velocity.y);
            }

            if (rb.velocity.y > moveSpeed * 1.5f)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed * 1.5f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        //check if is jumpshroom
        if (col.gameObject.layer == 8)
        {
            mushroomJump = true;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y) - new Vector3(transform.position.x, transform.position.y, 0);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;

            // And finally we add force in the direction of dir and multiply it by force. 
            rb.AddForce(dir * jumpForce * 1.5f, ForceMode2D.Impulse);        
        }

        // Check if is enemy
        if (col.gameObject.layer == 10)
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y) - new Vector3(transform.position.x, transform.position.y, 0);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;

            //Debug.Log(dir);
            // And finally we add force in the direction of dir and multiply it by force. 
            rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
        }
    }
}
