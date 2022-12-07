using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 target;
    public float speed;
    private int counter = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //get the mouse position
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = target - new Vector2(transform.position.x, transform.position.y);
        direction = direction.normalized;
        rb.velocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Wall")
        {
            counter++;
            if (counter >= 3)
            {
                Destroy(gameObject, 0.01f);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
           // col.gameObject.takeHit();
            Destroy(gameObject);
            
        }   
    }
    

    // Update is called once per frame
    void Update()
    {   //moves the bullet along to the mouse position


    }
}
