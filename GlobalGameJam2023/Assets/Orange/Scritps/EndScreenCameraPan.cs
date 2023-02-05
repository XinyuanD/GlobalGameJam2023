using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenCameraPan : MonoBehaviour
{
    public Transform startPoint;
    public float panSpeed = 5f;
    public bool stopPan = false;
    void Start()
    {
        transform.position = startPoint.position;
    }

    void Update()
    {
        if (!stopPan)
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WayPoint")
        {
            stopPan = true;
        }
    }
}
