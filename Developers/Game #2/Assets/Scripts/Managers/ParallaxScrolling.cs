using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private void FixedUpdate()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Parallax Object");

        foreach (GameObject parallaxObject in objects)
        {
            CalculateXParallax(parallaxObject);
            CalculateYParallax(parallaxObject);
        }
    }

    private void CalculateXParallax(GameObject parallaxObject)
    {
        float speed = Mathf.Abs(0.25f / parallaxObject.gameObject.transform.position.z);

        var x = Input.GetAxisRaw("Horizontal");

        Vector2 direction = new Vector2(x, 0f);

        parallaxObject.gameObject.transform.Translate(direction * speed);
    }

    private void CalculateYParallax(GameObject parallaxObject)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float speed = Mathf.Abs(0.25f / parallaxObject.gameObject.transform.position.z);

        float yVelocity = player.GetComponent<Rigidbody2D>().velocity.y;

        var dir = 0;

        if (yVelocity > 0) dir = 1; else if (yVelocity < 0) dir = 0;

        Vector2 direction = new Vector2(0f, dir);

        parallaxObject.gameObject.transform.Translate(direction * speed);
    }
}
