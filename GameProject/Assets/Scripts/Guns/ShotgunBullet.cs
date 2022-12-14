using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 target;
    public float speed;
    private int counter = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // since shotgun fires multiple bullets we don't want them to go to exact same point
        // instead we want all bullets to go parallel, so we take the guns rotation and apply
        // it to the bullets
        Vector2 direction = target - new Vector2(-Shotgun.targetDirection.x, -Shotgun.targetDirection.y);
        direction = direction.normalized;

        rb.velocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.collider.tag == "Wall")
        // {
        counter++;
        if (counter >= 3)
        {
            Destroy(gameObject, 0.01f);
        }
        // }
    }
}
