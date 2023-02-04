using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

    public Transform myTarget;

    public float xOffset;
    public float yOffset;
    public float zOffset = -10f;


	// Update is called once per frame
	void Update () {
        //If there is no target, camera goes into free move mode

        if (myTarget == null)
        {
            myTarget = GameObject.FindWithTag("Player").transform;
        }

        // This finds the z axis of the target and snaps to the targets z position.

        if (myTarget != null) {
			Vector3 targPos = myTarget.position;
			targPos.z = zOffset;
            targPos.x = targPos.x + xOffset;
            targPos.y = targPos.y + yOffset;

			// Consider using Vector3.Lerp for neat effects!
			transform.position = targPos;
		}
	}
}
