using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float initialWaitTime = 0.5f;
    private float waitTime = 0f;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        waitTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            waitTime = initialWaitTime;
            effector.rotationalOffset = 180f;
        }

        if (waitTime < 0f)
        {
            effector.rotationalOffset = 0f;
        }
    }
}
