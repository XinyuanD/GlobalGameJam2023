using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject wayPoint1;
    public GameObject wayPoint2;
    public BounceDetector bounceDetector;
    [SerializeField] private bool isStunned = false;
    public float secondsStunned = 2f;
    private float x1;
    private float x2;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public int direction = 1;

    public Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        x1 = wayPoint1.transform.position.x;
        x2 = wayPoint2.transform.position.x;

        float xPos = (x1 + x2) / 2;
        transform.position = new Vector2(xPos, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == wayPoint1 || other.gameObject == wayPoint2)
        {
            direction = -direction;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void Update()
    {
        if (bounceDetector != null && bounceDetector.gotBounced && !isStunned)
        {
            isStunned = true;
            anim.SetBool("Stunned", true);
            StartCoroutine(StunnedRoutine());
        }
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            anim.SetBool("Moving", true);
        }
    }

    IEnumerator StunnedRoutine()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(secondsStunned);
        bounceDetector.gotBounced = false;
        isStunned = false;
        anim.SetBool("Stunned", false);
    }
}
