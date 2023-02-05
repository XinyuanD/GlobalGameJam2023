using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDetector : MonoBehaviour
{
    public bool gotBounced = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gotBounced = true;
        }
    }

}
