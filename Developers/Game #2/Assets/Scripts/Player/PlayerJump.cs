using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Transform feetPosition;
    public LayerMask ground;
    public float timeInAir = 1.0f;
    public float jumpForce = 1.0f;

    private Rigidbody2D rb;
    private bool canJump;
    private bool isInAir;

    private float jumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        canJump = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Player Jump") == 1 && canJump)
        {
            isInAir = true;

            if (jumpTime < timeInAir)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime += Time.fixedDeltaTime;
            }
        } else
        {
            if (isInAir)
            {
                canJump = false;
            }
        }

        if (isInAir)
        {
            Collider2D overlapObj = Physics2D.OverlapCircle(feetPosition.position, 0.01f, ground);

            if (overlapObj != null)
            {
                if (ground == (ground | (1 << overlapObj.gameObject.layer)))
                {
                    Debug.Log("Aye");
                    isInAir = false;
                    canJump = true;
                    jumpTime = 0;
                }
            }
        }
    }
}