using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float maxHeighty;
    public float minHeighty;
    public float maxHeightx;
    public float minHeightx;
    public Transform target;


    public void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minHeightx, maxHeightx)+5, Mathf.Clamp(target.position.y, minHeighty, maxHeighty), -20);
    }
}
