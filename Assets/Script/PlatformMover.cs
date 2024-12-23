using System;
using System.Collections;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;

    public float waitTime = 2f;

    private bool moving = true;

    private float startX;
    private void Start()
    {
        moving = false;
        startX = transform.position.x;
        StartCoroutine(WaitAndMove());
    }

    public void Update()
    {
        if (moving)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x >= startX + distance)
            {
                moving = false;
            }
        }
    }

    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);
        moving = true;
        startX = transform.position.x;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    public void Stop()
    {
        moving = false;
        ResetPosition();
    }

    public void Resume()
    {
        moving = true;
    }
}
