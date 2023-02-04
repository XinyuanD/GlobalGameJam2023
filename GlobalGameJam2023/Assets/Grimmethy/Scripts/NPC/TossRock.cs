using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TossRock : MonoBehaviour
{
    public Transform target;

    public Transform firePoint;
    public GameObject projectileRock;

    public float timeBetweenShots = 3f;
    public float bulletDeviation = 3f;

    public float bulletSpeed = 150f;
    public float bulletLife = 2.5f;
    public float bulletSizeMod;
    public float bulletSize = 1f;

    public bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        StartCoroutine(ShotCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate Angle Between the collision point and the player
        Vector3 direction = target.transform.position - transform.position;
        float zAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        firePoint.rotation = Quaternion.Euler(0, 0, zAngle);

        if (canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // canShoot is set to false so we can put a delay between the shots the player fires
        canShoot = false;

        // spawn a rock at the fire point oriented the direction the player is facing

        GameObject bullet = (GameObject) Instantiate(projectileRock, firePoint.position, firePoint.rotation);

        float deviation = Random.Range(-bulletDeviation, bulletDeviation);
        bullet.transform.Rotate(0, 0, deviation);

        bullet.GetComponent<Ordinance>().speed = bulletSpeed;

        bullet.transform.localScale = new Vector3(bulletSize, bulletSize, 1);

        // starts a timer for when the player can shoot again
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void GetPlayerInput()
{
    // Fire a shot if the "Shoot" button (button we setup in the Unity Input Project Settings)
    // is held down AND the player can shoot - meaning the shot cooldown has elapsed
    if (canShoot)
    {
        Shoot();
    }
}
}
