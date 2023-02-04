using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script is attached to the Laser Blast prefab
/// When a Laser Blast is spawned, it will move forward until
/// the timeToLive expires or it collides with an object
/// </summary>
public class Ordinance : MonoBehaviour
{
    public string ordinanceName = "Rock";

    public float speed;                 // Speed at which the laser blast will travel
    public float timeToLive;            // Time in seconds that the blast will exist in the world
                                        // location of collision with another GameObject

    private Vector3 moveVector;         // Direction and speed the laser blast will travel
    public float damage;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        // Sets the direction of travel to move straight forward
        // Multiplied by speed and fixedDeltaTime to set the proper
        // speed independent of framerate
        moveVector = Vector3.up * speed * Time.fixedDeltaTime;

    }

    /// <summary>
    /// Called on a fixed time interval. We use this to move the laser blast independent of framerate
    /// </summary>
    private void FixedUpdate()
    {
        // Each fixed update step, move the laser blast straight forward at the specified speed
        transform.Translate(moveVector);
    }

    /// <summary>
    /// Called when this GameObject collides with another GameObject with a Collider2D component
    /// This will only be called when it collides with objects on physics layers that we
    /// specified in the Unity Physics 2D Project Settings.
    /// </summary>
    /// <param> Contains information about the collision between the two objects </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

