using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingBackground : MonoBehaviour
{
    public float speed;
    public Transform[] background;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float topPosY = 0f;
    float bottomPosY = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    Vector3 firstPosition;
    Vector3 lastPosition;

    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2 * background.Length;
        topPosY = yScreenHalfSize * 2;
        bottomPosY = yScreenHalfSize * 2 * background.Length;

        firstPosition = background[0].position;
        lastPosition = background[7].position;
    }

    void Update()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;

            if (background[i].position.y < lastPosition.y )
            {
                background[i].position = firstPosition;
            }
        }
    }
}
