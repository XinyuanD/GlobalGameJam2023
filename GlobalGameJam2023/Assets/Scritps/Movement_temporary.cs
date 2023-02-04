using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_temporary : MonoBehaviour
{
    private float moveInput;
    [SerializeField] float moveSpeed = 5;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
