using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour, IPauseable
{
    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    public void OnGamePause()
    {
        Debug.Log("I was paused");
    }

    public void OnGameResume()
    {

    }
}
