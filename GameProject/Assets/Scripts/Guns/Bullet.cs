using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
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
        }
        if (counter >= 3)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        if (col.collider.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerMovement>().takeHit();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
