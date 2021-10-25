using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCamera : MonoBehaviour
{
    public Transform follow;
    public float followSpeed;
    private void Update()
    {
        Vector3 followPos = new Vector3(follow.position.x, follow.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, followPos, 0.5f * followSpeed);
    }
}
