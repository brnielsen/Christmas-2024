using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRayDistance = 0.5f;

    [Header("Present Collision")]
    [SerializeField] private LayerMask presentCollisionLayer;
    [SerializeField] private Transform presentHoldPoint;

    private Present heldPresent;

    [Header("Dunk Collision")]

    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float startY;
    private bool isGrounded;
    private float verticalVelocity = 0f;

    void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector3.down, groundCheckRayDistance, groundLayer);
        // Add to Update() method after raycast:
        Debug.DrawRay(transform.position, Vector3.down * groundCheckRayDistance, isGrounded ? Color.green : Color.red);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            UpdateJump();
        }
        else if (!isGrounded)
        {
            // Apply gravity
            verticalVelocity += gravity * Time.deltaTime;
            Vector3 pos = transform.position;
            pos.y += verticalVelocity * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            verticalVelocity = 0f;
        }

        
    }



    private void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f;
        startY = transform.position.y;
        verticalVelocity = 0f;
    }

    private void UpdateJump()
    {
        jumpTimer += Time.deltaTime;
        float progress = jumpTimer / jumpDuration;

        if (progress >= 1f)
        {
            isJumping = false;
            return;
        }

        float height = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
        Vector3 pos = transform.position;
        pos.y = startY + height;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & presentCollisionLayer) != 0)
        {
            Debug.Log("Present Collision");
            // Handle present collision
            heldPresent = other.gameObject.GetComponent<Present>();
            other.gameObject.GetComponent<ArcMover>().presentHeld = true;

            other.gameObject.transform.SetParent(presentHoldPoint);
            other.gameObject.transform.localPosition = Vector3.zero;

            
            Chimney closestChimney = ChimneyManager.Instance.ClosestChimney(transform.position);
            Debug.Log("Closest Chimney: " + closestChimney);
            if (closestChimney != null)
            {
                if (Vector3.Distance(transform.position, closestChimney.DunkTarget.position) <= closestChimney.DunkRadius)
                {
                    Dunk();
                }else{
                    Miss();
                }
            }
        }
    }

    private void Miss()
    {
        Debug.Log("Miss");
        if (heldPresent != null){
            heldPresent.Miss();
            heldPresent = null;
        }
    }

    private void Dunk()
    {
        Debug.Log("Dunk");
        if (heldPresent != null){
            heldPresent.Dunk();
            heldPresent = null;
        }
    }
}
