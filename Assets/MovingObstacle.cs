using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private float speed;
    private float t; // Interpolation factor. When it reaches 1, the interpolation is complete.
    private bool reverse = false;


    void Update()
    {
        t += speed * Time.deltaTime;
        t = Math.Clamp(t, 0.0f, 1.0f);

        if(!reverse) {
            obstacleObject.transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
        } else {
            obstacleObject.transform.position = Vector3.Lerp(pointB.position, pointA.position, t);
        }

        if(t == 1.0) {
            reverse = !reverse; // Reverse the direction when obstacle reached it's point
            t = 0.0f;
        }

    }
}
