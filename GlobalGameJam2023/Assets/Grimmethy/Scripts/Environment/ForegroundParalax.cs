using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundParalax : MonoBehaviour
{
    public Vector3 startPos;

    [Header("Pick a number less than 1, the lower the number the faster the object moves")]
    public float paralaxValue = .5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        transform.position = startPos + cameraPos / paralaxValue;
    }
}