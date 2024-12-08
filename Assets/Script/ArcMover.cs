using System;
using UnityEngine;

public class ArcMover : MonoBehaviour
{
    [SerializeField] private float height = 5f;
    [SerializeField] private float duration = 1f;

    [SerializeField] private Transform _targetTransform;

    private Vector3 originalPosition;

    public bool presentHeld = false;

    public void Start()
    {
        originalPosition = transform.position;
    }
    public void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LaunchToTarget();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }

    public void LaunchInArc(Vector3 targetPosition)
    {
        StartCoroutine(MoveInArc(targetPosition));
    }

    [ContextMenu("Launch To Target")]
    public void LaunchToTarget()
    {
        if (_targetTransform != null)
        {
            LaunchInArc(_targetTransform.position);
        }
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        presentHeld = false;
    }

    private System.Collections.IEnumerator MoveInArc(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration && !presentHeld)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Calculate arc height using parabola
            float heightOffset = Mathf.Sin(t * Mathf.PI) * height;

            // Lerp from start to end position
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
            currentPos.y += heightOffset;

            transform.position = currentPos;
            yield return null;
        }

        transform.position = endPos;
    }
}