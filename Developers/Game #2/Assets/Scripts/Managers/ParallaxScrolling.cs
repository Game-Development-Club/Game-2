using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    private void FixedUpdate()
    {
        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.gameObject.transform.position.z == 0) return;

            float speed = Mathf.Abs(0.25f / sprite.gameObject.transform.position.z);

            var x = Input.GetAxisRaw("Horizontal");

            Vector2 direction = new Vector2(x, 0f);

            sprite.gameObject.transform.Translate(direction * speed);
        }
    }
}
