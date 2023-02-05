using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector2 checkPoint;

    private void Start()
    {
        checkPoint = transform.position;
    }

    public void Respawn()
    {
        transform.position = checkPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CheckPoint")
        {
            checkPoint = other.transform.position;
            Debug.Log("Check point set to: " + checkPoint);
        }
    }
}
